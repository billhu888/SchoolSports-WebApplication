using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSports.Models
{
    public class StudentsModel
    {
        public const string fStudent_ID = "Student_ID";
        public const string fFirst_Name = "First_Name";
        public const string fMiddle_Name = "Middle_Name";
        public const string fLast_Name = "Last_Name";
        public const string fSex = "Sex";
        public const string fGrade = "Grade";
        public const string fDate_of_Birth = "Date_of_Birth";
        public const string fAge = "Age";

        // Properties
        public int Student_ID { get; set; }
        public string First_Name { get; set; } = "";
        public string Middle_Name { get; set; } = "";
        public string Last_Name { get; set; } = "";
        public string Sex { get; set; } = "";
        public string Grade { get; set; } = "";
        public DateTime Date_of_Birth { get; set; }
        public int Age { get; set; }
    }
}