using System;
using System.IO;
using SQLite;

namespace FoodPin.Controller
{
    public class DataBaseConnection
    {
        private static DataBaseConnection dataBaseConnectionInstance;

        private DataBaseConnection()
        {
            Conn = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FoodPin.db3"));
        }

        public static DataBaseConnection Instance
        {
            get
            {
                if (dataBaseConnectionInstance == null)
                {
                    dataBaseConnectionInstance = new DataBaseConnection();
                }

                return dataBaseConnectionInstance;
            }
        }

        public SQLiteConnection Conn { get; }
    }
}
