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
