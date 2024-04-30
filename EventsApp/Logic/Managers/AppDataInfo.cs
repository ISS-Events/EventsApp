namespace EventsApp.Logic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class AppDataInfo
    {
        public const string AppFolder = "EventsApp";
        public const string UsersCSV = "UsersCSV.csv";
        public const string EventsCSV = "EventsCSV.csv";
        public const string DataBase = "Events.mdf";

        private static string id = "CloudSAd4cccee0";
        private static string password = "issevents_123";

        public static string PersistentDataPath => FileSystem.Current.AppDataDirectory;

        public static string AzureConnectionString => $"Server=tcp:iss-events.database.windows.net,1433;Initial Catalog=EventsDB;Persist Security Info=False;User ID={id};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static string ValidatePath(string path)
        {
            // Data\UsersCSV.csv will be converted to C:\Users\{username}\AppData\Roaming\EventsApp\Data\UsersCSV.csv
            // log.txt will be converted to C:\Users\{username}\AppData\Roaming\EventsApp\log.txt
            string dataFolder = FileSystem.Current.AppDataDirectory;

            // Check if path is already a full path
            if (!path.Contains(dataFolder))
            {
                path = Path.Combine(dataFolder, path);
            }

            // Create folders if they don't exist
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return path;
        }
    }
}
