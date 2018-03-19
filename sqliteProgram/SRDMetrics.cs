using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Metrics
{
    class SRDMetrics
    {

        public static int calculateDBMetrics(dbClass dbConnect)
        {
            string query = "SELECT Vehicle_Application FROM mainTable WHERE Vehicle_Application != NULL;";
            SQLiteDataReader read = dbConnect.submitQuery(query);
            int i = 0;
            var values = new List<Object>();
  
            while(read.Read())
            {
                Console.Write("Herro");
                Console.WriteLine(i);
                values.Add(read.GetValue(i));
//                Console.WriteLine(read.GetValue(i));
//                Console.WriteLine(read.GetString(i).ToString());
                i++;
            }
            var output = values.ToArray();

            for(i = 0; i < output.Length; i++)
            {
                Console.WriteLine(output[i]);
            }

            return 0;
        }
        
        public static void calculateMetrics(List<NodeLine> srdList)
        {
            double srdObjectCnt             = 0;
            double srdFeatureCtr            = 0;
            double srdRequirementCtr        = 0;
            double srdHeadingCtr            = 0;
            double srdFeatureThemeCtr       = 0;
            double srdFeatureGroupCtr       = 0;
            double srdDescriptionCtr        = 0;
            double srdRationaleCtr          = 0;
            double srdUseCaseDiagramCtr     = 0;
            double srdInfosCtr              = 0;
            double srdVehicleContextCtr     = 0;
            double srdEmptyObjTypeCtr       = 0;
            double srdNoFeatureOwnerCtr     = 0;
            double srdReqtNoRationaleCtr    = 0;
            double srdNoObjHeadandObjText   = 0;
            double srdReqtNoObjText         = 0;
            double srdHeadingNoObjHeading   = 0;
            double srdReqtBadObjLvl         = 0;
            double srdVehApplObsol          = 0;
            double srdReqtNoVehApp          = 0;
            double result                   = 0;
            double srdCascadedReqtCtr       = 0;
            double srdDerivedReqtCtr        = 0;
            double srdCascadedDerivedCtr    = 0;
            double incrementer              = 0;

            foreach (NodeLine car in srdList)
            {
                //Module Level Status Checks
                if (car.createdBy != "")                    { srdObjectCnt++; }         //SRD-MSC-01
                if (car.objectType == "Requirement")        { srdRequirementCtr++; }    //SRD-MSC-02 && SRD-MEC-16
                if (car.objectType == "Heading")            { srdHeadingCtr++; }        //SRD-MSC-03
                if (car.objectType == "Rationale")          { srdRationaleCtr++; }      //SRD-MSC-04
                if (car.objectType == "Description")        { srdDescriptionCtr++; }    //SRD-MSC-05
                if (car.objectType == "Feature Title")      { srdFeatureCtr++; }        //SRD-MSC-06
                if (car.objectType == "Info")               { srdInfosCtr++; }          //SRD-MSC-07
                if (car.objectType == "")                   { srdEmptyObjTypeCtr++; }   //SRD-MSC-08 && SRD-MEC-03
                if (car.objectType == "Vehicle Context")    { srdVehicleContextCtr++; } //SRD-MSC-09 && SRD-MEC-21
                if (car.objectType == "Feature Group")      { srdFeatureGroupCtr++; }   //SRD-MSC-10
                if (car.objectType == "Use Case Diagram")   { srdUseCaseDiagramCtr++; } //SRD-MSC-11 && SRD-MEC-20
                if (car.objectType == "Feature Theme")      { srdFeatureThemeCtr++; }   //SRD-MSC-12
                if (car.objectType == "Cascaded Reqt")      { srdCascadedReqtCtr++; }   //SRD-MSC-13
                if (car.objectType == "Derived Reqt")       { srdDerivedReqtCtr++; }    //SRD-MSC-14
                
                //Module Level Error Checks
                if ((car.objectType == "Feature Title") && (car.featureOwner == ""))                            { srdNoFeatureOwnerCtr++; }     //SRD-MEC-01
                if ((car.objectType == "Requirement") && (car.requirementRationale == ""))                      { srdReqtNoRationaleCtr++; }    //SRD-MEC-02
                if ((car.objectHeading == "") && (car.objectText == ""))                                        { srdNoObjHeadandObjText++; }   //SRD-MEC-04
                if ((car.objectType == "Requirement") && (car.objectText == ""))                                { srdReqtNoObjText++; }         //SRD-MEC-05
                if ((car.objectType == "Heading") && (car.objectHeading == ""))                                 { srdHeadingNoObjHeading++; }   //SRD-MEC-06
                //ToDo: SRD-MEC-07 Total Number of Requirements Without In-Links from the SID
                //ToDo: SRD-MEC-08 Total Number of Requirements Without In-Links from the SFD
                //ToDo: SRD-MEC-09 Total Number of Requirements Without In-Links from SFD Feature Titles
                //ToDo: SRD-MEC-10 Total Number of Requirements That Have Requirement Child Objects
                //ToDo: SRD-MEC-11 Total Number of Rationales That Have Requirement Child Objects
                //ToDo: SRD-MEC-12 Total Number of Rationales That Have Rationale Child Objects
                //ToDo: SRD-MEC-13 Total Number of Feature Titles That Have no Child Objects
                if ((car.objectType == "Requirement") && ((car.objectLevel <= 3) || (car.objectLevel >= 6)))    { srdReqtBadObjLvl++; }         //SRD-MEC-14
                //ToDo: SRD-MEC-15 Total Number of Requirements That Have Rationale Child Objects
                //ToDo: SRD-MEC-17 REQUIREMENT HEALTH CHECKS on Object text (Needs decomposing)
                if((car.vehicleApplication == "14.25My J#1")        ||
                   (car.vehicleApplication == "14.25My J#2 CHINA")  ||
                   (car.vehicleApplication == "16My")               ||
                   (car.vehicleApplication == "18My PHEV X1")       ||
                   (car.vehicleApplication == "18My X590 X1"))                                                  { srdVehApplObsol++; }          //SRD-MEC-18
                if ((car.objectType == "Requirement") && (car.vehicleApplication == ""))                        { srdReqtNoVehApp++; }          //SRD-MEC-19

                //for (int i = 0; i < 1000; i++)
                //{
                //    if ((car.objectType == "Requirement") && (car.objectType[i+1] == "Requirement") &&
                //    (car.objectLevel != car.objectLevel[i+1]))
                //    {
                //        Console.WriteLine(car.absoluteNumber[i+1] + " is at the incorrect level!");
                //        Console.ReadLine();
                //    }
                //}


                    //if ((car.objectType == "Requirement") && (car.sourceModules != ""))
                    //{
                    //    incrementer++;
                    //    Console.WriteLine("Requirement ID: " + car.absoluteNumber + " Has no link to the SID");
                    //    Console.ReadLine();
                    //    //SRD-MEC-07
                    //}

                car.metrics(); //Check each object to confirm that it has an attribute as expected
                car.printContents();
            }
            //Printing out Status/Error checks to the console (will be removed when data realised in text files/website
            Console.WriteLine("");
            Console.WriteLine("Module Level Status Checks");
            Console.WriteLine("[SRD-MSC-01] " + srdObjectCnt + " Objects in the SRD \t\t\t\t(100.00%)");
            result = Math.Round(((srdRequirementCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-02] " +srdRequirementCtr + " Requirements \t\t\t\t\t(" + result + "%)");
            result = Math.Round(((srdHeadingCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-03] " + srdHeadingCtr + " Headings \t\t\t\t\t(" + result + "%)");
            result = Math.Round(((srdRationaleCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-04] " + srdRationaleCtr + " Rationales \t\t\t\t\t(" + result + "%)");
            result = Math.Round(((srdDescriptionCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-05] " + srdDescriptionCtr + " Descriptions \t\t\t\t\t(" + result + "%)");
            result = Math.Round(((srdFeatureCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-06] " + srdFeatureCtr + " Feature Titles \t\t\t\t(" + result + "%)");
            result = Math.Round(((srdInfosCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-07] " + srdInfosCtr + " Information Statements \t\t\t(" + result + "%)");
            result = Math.Round(((srdEmptyObjTypeCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-08] " + srdEmptyObjTypeCtr + " Unassigned Objects \t\t\t\t(" + result + "%)");
            result = Math.Round(((srdVehicleContextCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-09] " + srdVehicleContextCtr + " Vehicle Contexts \t\t\t\t(" + result + "%)");
            result = Math.Round(((srdFeatureGroupCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-10] " + srdFeatureGroupCtr + " Feature Groups \t\t\t\t\t(" + result + "%)");
            result = Math.Round(((srdUseCaseDiagramCtr / srdObjectCnt) * 100),2);
            Console.WriteLine("[SRD-MSC-11] " + srdUseCaseDiagramCtr + " Use Case Diagrams \t\t\t\t(" + result + "%)");
            result = Math.Round(((srdFeatureThemeCtr / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MSC-12] " + srdFeatureThemeCtr + " Feature Themes \t\t\t\t\t(" + result + "%)");
            result = Math.Round(((srdCascadedReqtCtr / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MSC-13] " + srdCascadedReqtCtr + " Cascaded Requirements \t\t\t\t(" + result + "%)");
            result = Math.Round(((srdDerivedReqtCtr / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MSC-14] " + srdDerivedReqtCtr + " Derived Requirements \t\t\t\t(" + result + "%)");
            Console.WriteLine("");
            
            Console.WriteLine("Module Level Error Checks");
            result = Math.Round(((srdNoFeatureOwnerCtr / srdFeatureCtr) * 100), 2);
            Console.WriteLine("[SRD-MEC-01] " + srdNoFeatureOwnerCtr + " Features with no Feature Owner \t\t\t(" + result + "%)");
            result = Math.Round(((srdReqtNoRationaleCtr / srdRequirementCtr) * 100), 2);
            Console.WriteLine("[SRD-MEC-02] " + srdReqtNoRationaleCtr + " Requirements with no Requirement Rationale \t(" + result + "%)");
            result = Math.Round(((srdEmptyObjTypeCtr / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MEC-03] " + srdEmptyObjTypeCtr + " Objects with no Object Type \t\t\t(" + result + "%)");
            result = Math.Round(((srdNoObjHeadandObjText / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MEC-04] " + srdNoObjHeadandObjText + " Objects without an Object Heading & Object Text \t(" + result + "%)");
            result = Math.Round(((srdReqtNoObjText / srdRequirementCtr) * 100), 2);
            Console.WriteLine("[SRD-MEC-05] " + srdReqtNoObjText + " Requirements with no Object Text \t\t(" + result + "%)");
            result = Math.Round(((srdHeadingNoObjHeading / srdRequirementCtr) * 100), 2);
            Console.WriteLine("[SRD-MEC-06] " + srdHeadingNoObjHeading + " Headings with no Object Heading \t\t\t(" + result + "%)");
            Console.WriteLine("[SRD-MEC-07] Placeholder");
            Console.WriteLine("[SRD-MEC-08] Placeholder");
            Console.WriteLine("[SRD-MEC-09] Placeholder");
            Console.WriteLine("[SRD-MEC-10] Placeholder");
            Console.WriteLine("[SRD-MEC-11] Placeholder");
            Console.WriteLine("[SRD-MEC-12] Placeholder");
            Console.WriteLine("[SRD-MEC-13] Placeholder");
            result = Math.Round(((srdReqtBadObjLvl / srdRequirementCtr) * 100), 2);
            Console.WriteLine("[SRD-MEC-14] " + srdReqtBadObjLvl + " Requirements with incorrect Object level \t(" + result + "%)");
            Console.WriteLine("[SRD-MEC-15] Placeholder");
            srdCascadedDerivedCtr = srdCascadedReqtCtr + srdDerivedReqtCtr;
            result = Math.Round(((srdCascadedDerivedCtr / srdRequirementCtr) * 100), 2);
            Console.WriteLine("[SRD-MEC-16] " + srdCascadedDerivedCtr + " Requirements not set to Derived/Cascaded \t(" + result + "%)");
            Console.WriteLine("[SRD-MEC-17] Placeholder");
            result = Math.Round(((srdVehApplObsol / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MEC-18] " + srdVehApplObsol + " Objects with Obsolete Vehicle Applications \t(" + result + "%)");
            result = Math.Round(((srdReqtNoVehApp / srdRequirementCtr) * 100), 2);
            Console.WriteLine("[SRD-MEC-19] " + srdReqtNoVehApp + " Requirements with No Vehicle Application \t(" + result + "%)");
            result = Math.Round(((srdUseCaseDiagramCtr / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MEC-20] " + srdUseCaseDiagramCtr + " Objects categorised as Use Case Diagrams \t(" + result + "%)");
            result = Math.Round(((srdVehicleContextCtr / srdObjectCnt) * 100), 2);
            Console.WriteLine("[SRD-MEC-21] " + srdVehicleContextCtr + " Objects categorised as vehicle Context \t\t(" + result + "%)");           
            Console.WriteLine("");

        }
    }
}
