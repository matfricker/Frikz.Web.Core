using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Frikz.Web.Core
{
    public class Person
    {
        Int32 PersonID;
        string FirstName;
        string MiddleName;
        string LastName;
        string DayNumber;
        string MobileNumber;
        string EmailAddress;
        string PostalAddress;
        string PostCode;
        bool Active;
                
        public DataSet dsPersonCollection;

        public Person()
        {
            dsPersonCollection = new DataSet("Person");

            DataTable dtPerson = new DataTable("DT_PERSON");
            DataTable dtPersonDetails = new DataTable("DT_PERSON_DETAILS");
            DataTable dtPersonAvailablity = new DataTable("DT_PERSON_AVAILABILITY");
            DataTable dtPersonSkills = new DataTable("DT_PERSON_SKILLS");

            //PERSON TABLE
            dtPerson.Columns.Add("PersonId", typeof(Int32));
            //dtPerson.Columns.Add("UserId", typeof(Guid));
            //dtPerson.Columns.Add("RoleId", typeof(Guid));
            dtPerson.Columns.Add("FirstName", typeof(String));
            dtPerson.Columns.Add("MiddleName", typeof(String));
            dtPerson.Columns.Add("LastName", typeof(String));
            dtPerson.Columns.Add("DayNumber", typeof(Int32));
            dtPerson.Columns.Add("MobileNumber", typeof(Int32));
            dtPerson.Columns.Add("EmailAddress", typeof(String));
            dtPerson.Columns.Add("PostalAddress", typeof(String));
            dtPerson.Columns.Add("PostCode", typeof(String));
            dtPerson.Columns.Add("Active", typeof(Boolean));
            dtPerson.Columns.Add("Type", typeof(Int32));

            //PERSON DETAILS TABLE
            dtPersonDetails.Columns.Add("PersonId", typeof(Int32));
            dtPersonDetails.Columns.Add("DateOfBirth", typeof(DateTime));
            dtPersonDetails.Columns.Add("Occupation", typeof(String));
            dtPersonDetails.Columns.Add("PlaceOfBirth", typeof(String));
            dtPersonDetails.Columns.Add("Nationality", typeof(String));
            dtPersonDetails.Columns.Add("Sex", typeof(Boolean));
            dtPersonDetails.Columns.Add("LocationPref", typeof(String));
            dtPersonDetails.Columns.Add("HoursPref", typeof(String));
            dtPersonDetails.Columns.Add("Qualifications", typeof(String));
            dtPersonDetails.Columns.Add("Reference1", typeof(String));
            dtPersonDetails.Columns.Add("Reference2", typeof(String));
            dtPersonDetails.Columns.Add("OwnTransport", typeof(Boolean));
            dtPersonDetails.Columns.Add("Level2SaftyExpiry", typeof(DateTime));
            dtPersonDetails.Columns.Add("PersonalLicenseExpiry", typeof(DateTime));
            dtPersonDetails.Columns.Add("OtherCertification", typeof(String));
            dtPersonDetails.Columns.Add("ContactDay", typeof(Int32));
            dtPersonDetails.Columns.Add("ContactEvening", typeof(Int32));
            dtPersonDetails.Columns.Add("SkillSummary", typeof(String));
                        
            //PERSON AVAILABILITY TABLE
            dtPersonAvailablity.Columns.Add("PersonId", typeof(Int32));
            dtPersonAvailablity.Columns.Add("Available", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("FreeAnytime", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("DayTime", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("Evenings", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("Weekends", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("FullTime", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("FlexFront", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("FlexBack", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("CustomerService", typeof(Boolean));
            dtPersonAvailablity.Columns.Add("Permanent", typeof(Boolean));
            
            //PERSON SKILLS TABLE
            dtPersonSkills.Columns.Add("PersonId", typeof(Int32));
            dtPersonSkills.Columns.Add("SkillType", typeof(Int32));
            dtPersonSkills.Columns.Add("Skill", typeof(String));
            dtPersonSkills.Columns.Add("Experience", typeof(Boolean));
            dtPersonSkills.Columns.Add("Interest", typeof(Boolean));
        }
    }
}
