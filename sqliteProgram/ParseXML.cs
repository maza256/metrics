using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Linq.Expressions;

namespace Metrics
{
    class ParseXML
    {
        static string[] Attributes = { "Absolute_Number", "Object_Level", "ASIL", "Created_By", "Created_On", "Created_Thru", "Feature_Owner", "Last_Modified_By", "Last_Modified_On", "Object_Heading",
                                     "Object_Text", "Object_Type", "Requirement_Rationale", "Reviewed_Flag", "Vehicle_Application"};

        static string[] attributeTypes = { "integer PRIMARY KEY", "text NULL", "text NULL","text NULL","text NULL","text NULL","text NULL",
                                           "text NULL","text NULL","text NULL","text NULL","text NULL","text NULL","text NULL","text NULL","text NULL"};


        public static int StoreinSQLite(dbClass dbConnect, string PROJECT_PATH)
        {
            //Create the two tables in the SQLite database.
            //Only needed to be run once
            createTables(dbConnect);

            XmlDocument xmlDoc = new XmlDocument(); //Create an XML document object
            xmlDoc.Load(PROJECT_PATH + "SRD.xml"); //Load the XML document from the specified file

            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("Object_Line"); //Identify each Object_Line object in the document

            foreach (XmlNode node in nodes)
            {
                int i;
                int absoluteNumber = Int32.Parse(node.SelectSingleNode("Absolute_Number").InnerText);
                int objectLevel = Int32.Parse(node.SelectSingleNode("Object_Level").InnerText);

                string command = "INSERT INTO mainTable" + " ( " + Attributes[0] + ", " + Attributes[1];
                string values = ") VALUES ( " + absoluteNumber + ", " + objectLevel;
                for (i = 2; i < Attributes.Length; i++)
                {
                    string s = node.SelectSingleNode(Attributes[i]).InnerText;
                    if (s != "")
                    {

                        command += ", " + Attributes[i];
                        values += ", '" + s + "'";
                    }
                }

                dbConnect.submitCommand(command + values + ");");
                XmlNodeList xml = node.SelectNodes("Source_Modules/Source_Module");
                string sa = "";
                i = 0;
                foreach (XmlNode srcMod in xml)
                {
                    string name = srcMod.ChildNodes[0].Value;
                    XmlNodeList SrcObjNums = srcMod.SelectNodes("Source_Object_Number");
                    foreach (XmlNode Obj in SrcObjNums)
                    {

                        command = "INSERT INTO sourceModules (Source_Module, Absolute_Number, Object_Number) VALUES ( ";
                        command += absoluteNumber.ToString() + ", '" + name + "', " + Obj.InnerText + ");";
                        dbConnect.submitCommand(command);
                    }
                    i++;
                }
            }
            return 1;
        }

        public static dbClass createTables(dbClass dbConnect)
        {
            string table_name = "mainTable";
            //Command to create Table with each of above properties
            string command = "CREATE TABLE IF NOT EXISTS " + table_name + " ( " + Attributes[0] + " " + attributeTypes[0];
            for (int i = 1; i < Attributes.Length; i++)
            {
                command += ", " + Attributes[i] + " " + attributeTypes[i];
            }

            dbConnect.submitCommand(command + ");");

            table_name = "sourceModules";
            command = "CREATE TABLE IF NOT EXISTS " + table_name + " ( Absolute_Number integer NULL, " +
                                                    " Source_Module   text    NULL, " +
                                                    " Object_Number   integer NULL );";
            dbConnect.submitCommand(command);
            return (dbConnect);
        }
    }
}

