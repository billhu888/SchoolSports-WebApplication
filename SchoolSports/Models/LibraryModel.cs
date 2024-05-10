using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSports.Models
{
    public class LibraryModel
    {
        public const string fBook_Id = "Book_Id";
        public const string fBook_Title = "Book_Title";
        public const string fBook_Author = "Book_Author";
        public const string fStudent_ID_Checked_Out = "Student_ID_Checked_Out";

        public int Book_Id { get; set; }
        public string Book_Title { get; set; }
        public string Book_Author { get; set; }
        public int? Student_ID_Checked_Out { get; set; }
    }
}