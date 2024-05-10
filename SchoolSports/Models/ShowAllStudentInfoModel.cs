using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSports.Models
{
    public class ShowAllStudentInfoModel
    {
        public StudentsModel StudentsModel { get; set; }
        public List<SportsModel> ListSports { get; set; }
        public List<LibraryModel> ListLibrary { get; set; }
    }
}