using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

//ToDo:
// - Fix how child nodes are read in the XML
//Find out how to access dats from tables using c#
//Once data can be read from tables, should be able to write into tables

namespace Metrics
{
    partial class MainClass
    {
        public const string PROJECT_PATH = @"C:\Users\maza2\Desktop\xml\";

        public static void Main(string[] args)
        {
            
            AscToXML.ASCToXMLConvert(PROJECT_PATH); //Clean up DXL output of illegal characters and then write to XML
           // List<NodeLine> srdList = ParseXML.ParseXMLFile(PROJECT_PATH);
           // SQLiteConnection m_dbConnection = new SQLiteConnection;
            
            //Create a db Connection class
            dbClass dbConnect = new dbClass("doors");
            ParseXML.StoreinSQLite(dbConnect, PROJECT_PATH);
            
            //SQLiteConnection m_dbConnection = DatabaseAccess.ParseXMLFile(PROJECT_PATH);
          //  SRDMetrics.calculateMetrics(srdList);
            SRDMetrics.calculateDBMetrics(dbConnect);
            Console.WriteLine("The end of the code hath been reached!!!"); //Indicates end of code reached
            Console.Read(); //Stops console from closing
        }
    }
}

/* 
            //Create a new sqlite file
            SQLiteConnection.CreateFile("Doors.sqlite");
            //Connect to the sqlite file
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Doors.sqlite;Version=3;");
            m_dbConnection.Open();

            //command that will create the tables
            string sql = "create table highscores (name varchar(20), score int)";
            //Execute the command
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            //New command to insert data
            sql = "insert into highscores (name, score) values ('Me', 9001)";
            //Execute Command
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            //Must close the database before exiting.
            m_dbConnection.Close();
*/