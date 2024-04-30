namespace EventsApp.Logic.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Attributes;
    using EventsApp.Logic.Managers;
    using Microsoft.Data.SqlClient;

    public class DataBaseAdapter<T>(string filePath)
        : DataAdapter<T>(filePath)
        where T : struct
    {
        private string connectionString = filePath; // CSV file path

        private string TableName => typeof(T).GetCustomAttributes(typeof(TableAttribute), true).Cast<TableAttribute>().FirstOrDefault().TableName;

        public string ConnectionString()
        {
            return connectionString;
        }

        public override void Add(T item)
        {
            string fields = this.GetFields();
            string values = this.GetValues(item);
            string insertUserQuery = $"INSERT INTO {this.TableName} ({fields}) VALUES" + $"({values})";
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(insertUserQuery, connection);
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

        public override void Clear()
        {
            string clearQuery = $"DELETE FROM {this.TableName}";
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(clearQuery, connection);
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

        public override void Connect()
        {
        }

        public override bool Contains(Identifier id)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string selectQuery = $"SELECT * FROM {this.TableName} WHERE ";
            foreach (var pk in pks)
            {
                selectQuery += $"{pk.Key} = '{pk.Value}' AND ";
            }

            selectQuery = selectQuery.Substring(0, selectQuery.Length - 5);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    return reader.HasRows;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return false;
        }

        public override void Delete(Identifier id)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string deleteQuery = $"DELETE FROM {this.TableName} WHERE ";
            foreach (var pk in pks)
            {
                deleteQuery += $"{pk.Key} = '{pk.Value}' AND ";
            }

            deleteQuery = deleteQuery.Substring(0, deleteQuery.Length - 5);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
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

        public override T Get(Identifier id)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string selectQuery = $"SELECT * FROM {this.TableName} WHERE ";
            foreach (var pk in pks)
            {
                selectQuery += $"{pk.Key} = '{pk.Value}' AND ";
            }

            selectQuery = selectQuery.Substring(0, selectQuery.Length - 5);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        T instance = default(T);
                        Type type = typeof(T);
                        TypedReference reference = __makeref(instance);

                        foreach (var property in type.GetFields())
                        {
                            if (property.FieldType.IsEnum || property.IsLiteral)
                            {
                                continue;
                            }

                            if (property.FieldType == typeof(string))
                            {
                                property.SetValueDirect(reference, reader[property.Name].ToString());
                            }
                            else if (property.FieldType == typeof(Guid))
                            {
                                property.SetValueDirect(reference, Guid.Parse(reader[property.Name].ToString()));
                            }
                            else if (property.FieldType == typeof(DateTime))
                            {
                                property.SetValueDirect(reference, DateTime.Parse(reader[property.Name].ToString()));
                            }
                            else if (property.FieldType == typeof(int))
                            {
                                property.SetValueDirect(reference, int.Parse(reader[property.Name].ToString()));
                            }
                            else if (property.FieldType == typeof(float))
                            {
                                property.SetValueDirect(reference, float.Parse(reader[property.Name].ToString()));
                            }
                            else
                            {
                                property.SetValueDirect(reference, reader[property.Name]);
                            }
                        }

                        return instance;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return default;
        }

        public override List<T> GetAll()
        {
            List<T> list = new List<T>();

            string selectAllQuery = $"SELECT * FROM {this.TableName}";

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(selectAllQuery, connection);
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        T instance = default(T);
                        Type type = typeof(T);
                        TypedReference reference = __makeref(instance);

                        foreach (var property in type.GetFields())
                        {
                            if (property.FieldType.IsEnum || property.IsLiteral)
                            {
                                continue;
                            }

                            if (property.FieldType == typeof(string))
                            {
                                property.SetValueDirect(reference, reader[property.Name].ToString());
                            }
                            else if (property.FieldType == typeof(Guid))
                            {
                                property.SetValueDirect(reference, Guid.Parse(reader[property.Name].ToString()));
                            }
                            else if (property.FieldType == typeof(DateTime))
                            {
                                property.SetValueDirect(reference, DateTime.Parse(reader[property.Name].ToString()));
                            }
                            else if (property.FieldType == typeof(int))
                            {
                                property.SetValueDirect(reference, int.Parse(reader[property.Name].ToString()));
                            }
                            else if (property.FieldType == typeof(float))
                            {
                                property.SetValueDirect(reference, float.Parse(reader[property.Name].ToString()));
                            }
                            else
                            {
                                property.SetValueDirect(reference, reader[property.Name]);
                            }
                        }

                        list.Add(instance);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return list;
        }

        public override void Update(Identifier id, T item)
        {
            Dictionary<string, object> pks = id.PrimaryKeys;

            string updateQuery = $"UPDATE {this.TableName} SET ";
            Type type = typeof(T);
            foreach (var property in type.GetFields())
            {
                if (property.FieldType.IsEnum || property.IsLiteral)
                {
                    continue;
                }

                if (property.FieldType == typeof(string))
                {
                    updateQuery += $"{property.Name} = '{property.GetValue(item)}', ";
                }
                else if (property.FieldType == typeof(Guid))
                {
                    updateQuery += $"{property.Name} = '{property.GetValue(item)}', ";
                }
                else if (property.FieldType == typeof(DateTime))
                {
                    updateQuery += $"{property.Name} = '{property.GetValue(item)}', ";
                }
                else
                {
                    updateQuery += $"{property.Name} = {property.GetValue(item)}, ";
                }
            }

            updateQuery = updateQuery.Substring(0, updateQuery.Length - 2);

            updateQuery += " WHERE ";
            foreach (var pk in pks)
            {
                updateQuery += $"{pk.Key} = '{pk.Value}' AND ";
            }

            updateQuery = updateQuery.Substring(0, updateQuery.Length - 5);

            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
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

        private string GetFields()
        {
            string fields = string.Empty;
            Type type = typeof(T);
            foreach (var property in type.GetFields())
            {
                // Skip if enum
                if (property.FieldType.IsEnum || property.IsLiteral)
                {
                    continue;
                }

                fields += $"{property.Name}, ";
            }

            return fields.Substring(0, fields.Length - 2);
        }

        private string GetValues(object o)
        {
            string values = string.Empty;
            Type type = typeof(T);

            foreach (var property in type.GetFields())
            {
                // If enum or const
                if (property.FieldType.IsEnum || property.IsLiteral)
                {
                    continue;
                }

                if (property.FieldType == typeof(string))
                {
                    values += $"'{property.GetValue(o)}', ";
                }
                else if (property.FieldType == typeof(Guid))
                {
                    values += $"'{property.GetValue(o)}', ";
                }
                else if (property.FieldType == typeof(DateTime))
                {
                    values += $"'{property.GetValue(o)}', ";
                }
                else
                {
                    values += $"{property.GetValue(o)}, ";
                }
            }

            return values.Substring(0, values.Length - 2);
        }
    }
}
