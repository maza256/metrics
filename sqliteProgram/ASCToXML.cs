using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Metrics
{
    public static class AscToXML
    {
        public static void ASCToXMLConvert(string PROJECT_PATH)
        {
            var xml = File.ReadAllText(PROJECT_PATH + "SRD.asc"); //read xml doc as a text file (so illegal characters can be removed)

            //Replacing the illegal characters 
            var fixedXml = xml.Replace(((char)0x91).ToString(), "'")
                              .Replace(((char)0x92).ToString(), "'")
                              .Replace(('&').ToString(), "&amp;")
                              .Replace(("<tbd>").ToString(), "tbd")
                              .Replace(("<Cell Voltage vs SOC>").ToString(), "Cell Voltage vs SOC")
                              .Replace(("<=").ToString(), "less than or equal to")
                              .Replace(("[<").ToString(), "[less than")
                              .Replace(("PM<7").ToString(), "Power Mode less than 7")
                              .Replace(("<insert martix here>").ToString(), "(insert martix here)")
                              .Replace(("Shuffle = <").ToString(), "Shuffle = less than")
                              .Replace(("< Interaction").ToString(), "Interaction")
                              .Replace(("entry speed >").ToString(), "entry speed")
                              .Replace(("< PM7").ToString(), "Power Mode less than 7")
                              .Replace(("<Default>").ToString(), "(Default)")
                              .Replace(("<Default 100 Nm tbc>").ToString(), "(Default 100 Nm tbc)")
                              .Replace(("< 15kph").ToString(), "less than 15kph")
                              .Replace(("<IMAGE 1>").ToString(), "(IMAGE 1)")
                              .Replace(("<IMAGE 2>").ToString(), "(IMAGE 2)")
                              .Replace(("<IMAGE3>").ToString(), "(IMAGE 3)")
                              .Replace(("<Image 4>").ToString(), "(IMAGE 4)")
                              .Replace(("<<").ToString(), "")
                              .Replace((">>").ToString(), "")
                              .Replace(("< 50 rpm").ToString(), "less than 50 rpm")
                              .Replace(("<10seconds").ToString(), "less than 10seconds")
                              .Replace(("< 0.5g").ToString(), "less than 0.5g")
                              .Replace(("<0.5g").ToString(), "less than 0.5g")
                              .Replace(("<0.3g").ToString(), "less than 0.3g")
                              .Replace(("< 0.3g").ToString(), "less than 0.3g")
                              .Replace(("<60").ToString(), "less than 60")
                              .Replace(("<30").ToString(), "less than 30")
                              .Replace(("<PSC>").ToString(), "PSC")
                              .Replace(("<PTCC>").ToString(), "PTCC")
                              .Replace(("<M1DJ>").ToString(), "M1DJ");
            
            File.WriteAllText(PROJECT_PATH + "SRD.xml", fixedXml);
        }
    }
}
