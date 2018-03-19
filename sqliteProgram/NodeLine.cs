using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics
{
    public class NodeLine
    {
        public string ASIL;
        public string createdBy;
        public string createdOn;
        public string createdThru;
        public string featureOwner;
        public string lastModifiedBy;
        public string lastModifiedOn;
        public string objectHeading;
        public string objectNumber;
        public string objectText;
        public string objectType;
        public string requirementRationale;
        public string reviewedFlag;
        public string vehicleApplication;
        public List<string> sourceModule;
        public string sourceModules;
        public string targetModule;
        public string targetModules;
        public List<string> srcObjNum;

        public int absoluteNumber;
        public int objectLevel;
        public int srdObjectCnt         = 0;
        public int srdFeatureCtr        = 0;
        public int srdRequirementCtr    = 0;
        public int srdHeadingCtr        = 0;
        public int srdFeatureThemeCtr   = 0;
        public int srdFeatureGroupCtr   = 0;
        public int srdDescriptionCtr    = 0;
        public int srdRationaleCtr      = 0;
        public int srdUseCaseDiagramCtr = 0;
        public int srdInfosCtr          = 0;
        public int srdVehicleContextCtr = 0;
        public int srdEmptyObjTypeCtr   = 0;

        public void metrics() //Method for metric checks
        {
            //Feature Level Status Checks - To be reported to Feature Owners
            if ((objectType == "Feature Title") && (objectHeading != ""))
            {
                string ftObjNo = objectNumber; //Get Object Number of the feature Title
                //find all requirements starting with this Object Number..........
            }

            //Feature Level Error Checks - To be reported to Feature Owners
            //Theme Level Metrics - To be reported to Tribe Leads
            //Program Level Metrics - To be reported to System leads
            //Module Level Status Checks - To be reported to Requirements Manager
            //Module Level Error Checks - To be reported to Requirements Manager
            if ((objectType == "Feature Title") && (featureOwner == "")) //Module Level Error Check 1
                Console.WriteLine("SRD-" + absoluteNumber + " Feature Title: " + objectHeading + " Needs a Feature Owner");
            if ((objectType == "Requirement") && (requirementRationale == "")) //Module Level Error Check 2
                Console.WriteLine(absoluteNumber + " Requirement without a Requirement Rationale");
            if ((objectType == "Requirement") && (sourceModules == "")) //Module Level Error Check 3
                Console.WriteLine(absoluteNumber + " Requirement without an In-Link");
            if (objectType == "") //Module Level Error Check 4
                Console.WriteLine(absoluteNumber + " Objects without an Object Type");
            if ((objectHeading == "") && (objectText == "")) //Module Level Error Check 5
                Console.WriteLine(absoluteNumber + " Objects without an Object Heading & Object Text");
        }
        public void printContents() //Method to print all information contained
        {
            Console.WriteLine(": " + absoluteNumber + ", D: " + objectHeading + ", A:" + createdBy);
            Console.WriteLine("There are " + srdObjectCnt + " objects in the SRD");
        }
    }
}
