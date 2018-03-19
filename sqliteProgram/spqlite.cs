using System.Data.SQLite;
using System;
using System.IO;

namespace Metrics {
   
    class dbClass 
    {
        private SQLiteConnection m_dbConnection;
        private string dbname;
        
        public dbClass(string name)
        {
            dbname = name;
            if (File.Exists(name + ".sqlite"))
            {
                File.Delete(name + ".sqlite");
                createFile();
            }
                
        }
        
        void createFile()
        {
            SQLiteConnection.CreateFile(dbname + ".sqlite");
            openDB();
        }
        
        private void openDB()
        {
            m_dbConnection = new SQLiteConnection("Data Source=" + dbname + ".sqlite;Version=3;");
            m_dbConnection.Open();
        }

        void closeDB()
        {
            m_dbConnection.Close();
        }
        
        //Need to return something
        public int submitCommand(string commandString)
        {
            SQLiteCommand command = new SQLiteCommand(commandString, m_dbConnection);
            return command.ExecuteNonQuery();
        }
        
        public SQLiteDataReader submitQuery(string query)
        {
            SQLiteCommand cmd = (SQLiteCommand)m_dbConnection.CreateCommand();
            cmd.CommandText = query;
            SQLiteDataReader sqReader = cmd.ExecuteReader();
            return sqReader;
        }   
    }
}