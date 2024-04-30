namespace EventsApp.Logic.Managers
{
    using System;
    using System.Data;
    using System.Reflection;
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Attributes;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;
    using Microsoft.Data.SqlClient;

    public static class ManagersInitializer
    {
        public static string ConnectionString;

        public static void Initialize()
        {
            // Setting up connections
            // CSVAdapter<UserInfo> usersCSVAdapter = new CSVAdapter<UserInfo>("UsersCSV");
            // CSVAdapter<Entities.EventInfo> eventsCSVAdapter = new CSVAdapter<Entities.EventInfo>("EventsCSV");
            // usersCSVAdapter.Connect();
            // eventsCSVAdapter.Connect();
            bool regenerateDB = false;

            SetupDB(true, true, regenerateDB);

            DataBaseAdapter<UserInfo> usersAdapter = new DataBaseAdapter<UserInfo>(ConnectionString);
            DataBaseAdapter<Entities.EventInfo> eventsAdapter = new DataBaseAdapter<Entities.EventInfo>(ConnectionString);
            DataBaseAdapter<ReportInfo> reportsAdapter = new DataBaseAdapter<ReportInfo>(ConnectionString);
            DataBaseAdapter<ReviewInfo> reviewsAdapter = new DataBaseAdapter<ReviewInfo>(ConnectionString);
            DataBaseAdapter<AdminInfo> adminsAdapter = new DataBaseAdapter<AdminInfo>(ConnectionString);
            DataBaseAdapter<UserEventRelationInfo> userEventRelationsAdapter = new DataBaseAdapter<UserEventRelationInfo>(ConnectionString);
            DataBaseAdapter<DonationInfo> donationsAdapter = new DataBaseAdapter<DonationInfo>(ConnectionString);

            UsersManager.Initialize(usersAdapter, adminsAdapter, userEventRelationsAdapter);
            EventsManager.Initialize(eventsAdapter, userEventRelationsAdapter);
            ReportsManager.Initialize(reportsAdapter);
            ReviewsManager.Initialize(reviewsAdapter);
            DonationsManager.Initialize(donationsAdapter);

            if (regenerateDB)
            {
                Dummy.Populate();
            }
        }

        public static void SetupDB(bool wipeExisting = false, bool azure = true, bool dropTables = false)
        {
            if (!azure)
            {
                string dbPath = AppDataInfo.ValidatePath("EventsDB.mdf");
                string logPath = AppDataInfo.ValidatePath("EventsDB.ldf");

                // Construct the connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(localdb)\\MSSQLLocalDB"; // or specify your SQL Server instance
                builder.InitialCatalog = "master";
                builder.IntegratedSecurity = false; // Use Windows Authentication
                builder.TrustServerCertificate = true; // Trust the server certificate
                ConnectionString = builder.ConnectionString;

                // Azure connection string
                if (!wipeExisting)
                {
                    return;
                }

                // SQL command to create the database
                string dropDatabaseQuery = "DROP DATABASE IF EXISTS EventsDB";
                string createDatabaseQuery = $"CREATE DATABASE EventsDB ON PRIMARY (NAME = EventsDB, FILENAME = '{dbPath}')";

                // Drop the database if it already exists
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(dropDatabaseQuery, connection);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                // Create connection and command objects
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(createDatabaseQuery, connection);
                    try
                    {
                        // Open the connection and execute the command
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Already exists
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            ConnectionString = AppDataInfo.AzureConnectionString;

            // ------------------- Create tables -------------------
            if (dropTables)
            {
                string namespaceName = "EventsApp.Logic.Entities";

                var assembly = Assembly.GetExecutingAssembly();
                var types = assembly.GetTypes();

                var structsInNamespace = types
                    .Where(t => t.Namespace == namespaceName && t.IsValueType && !t.IsEnum)
                    .ToList();

                foreach (var structType in structsInNamespace)
                {
                    string tableName = structType.GetCustomAttribute<TableAttribute>().TableName;
                    // Drop table if exists
                    string dropTableQuery = $"DROP TABLE IF EXISTS {tableName}";

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(dropTableQuery, connection);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    List<string> columns = new List<string>();

                    // Get only structs with no consts or enums
                    foreach (var field in structType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        bool primaryKey = field.GetCustomAttribute<PrimaryKeyAttribute>() != null;
                        string columnName = field.Name;
                        string columnType = GetFieldType(field.FieldType);

                        string column = $"{columnName} {columnType}";

                        columns.Add(column);
                    }

                    // Add primery keys
                    string primaryKeyString = "PRIMARY KEY (";
                    foreach (var field in structType.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (field.GetCustomAttribute<PrimaryKeyAttribute>() != null)
                        {
                            primaryKeyString += field.Name + ", ";
                        }
                    }

                    primaryKeyString = primaryKeyString.Remove(primaryKeyString.Length - 2) + ")";

                    columns.Add(primaryKeyString);

                    string createTableQuery = $"CREATE TABLE {tableName} ({string.Join(", ", columns)})";

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        SqlCommand command = new SqlCommand(createTableQuery, connection);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        private static string GetFieldType(Type fieldType)
        {
            if (fieldType == typeof(int))
            {
                return "INT";
            }
            else if (fieldType == typeof(string))
            {
                return "NVARCHAR(255)";
            }
            else if (fieldType == typeof(DateTime))
            {
                return "DATETIME";
            }
            else if (fieldType == typeof(float))
            {
                return "FLOAT";
            }
            else if (fieldType == typeof(bool))
            {
                return "BIT";
            }
            else
            {
                return "NVARCHAR(255)";
            }
        }
    }
}
