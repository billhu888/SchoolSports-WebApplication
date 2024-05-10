using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSports.Models;
using System.Data;
using Humanizer;
using SchoolSports.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchoolSports.Repositories
{
    class EditedStudentProfileRepo : BaseRepo
    {
        public bool UpdateStudent(StudentsModel studentProfile)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    String sql = 
                        $" UPDATE STUDENTS " +
                        $" SET Student_ID = '{studentProfile.Student_ID}', " +
                        $" First_Name = '{studentProfile.First_Name}', " +
                        $" Middle_Name = '{studentProfile.Middle_Name}', " +
                        $" Last_Name = '{studentProfile.Last_Name}', " +
                        $" Sex = '{studentProfile.Sex}', " +
                        $" Date_of_Birth = '{studentProfile.Date_of_Birth}' " +
                        $" WHERE Student_ID = '{studentProfile.Student_ID}' ";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();

                    success = true;
                }
                else
                {
                    Console.WriteLine("Failed to connect to database");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return success;
        }
    }
}