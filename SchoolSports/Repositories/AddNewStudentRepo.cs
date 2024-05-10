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
    class AddNewStudentRepo : BaseRepo    
    {
        public bool GetNewStudentID (StudentsModel studentprofile)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    String sqlGetMaxId = 
                        $" SELECT MAX(Student_ID) AS MaxStudentID  " +
                        $" FROM STUDENTS ";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sqlGetMaxId, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0) 
                    {
                        DataRow row1 = dt.Rows[0];

                        studentprofile.Student_ID = (int)row1["MaxStudentID"] + 1;
                    }
                }
                else
                {
                    Console.WriteLine("Failed to connect to database");
                }

                success = true;
            }
            catch (Exception e) 
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return success;
          
        }

        public bool AddNewStudent(StudentsModel StudentProfile)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    String sql =
                        $" INSERT INTO STUDENTS " +
                        $" (Student_ID, First_Name, Middle_Name, Last_Name, Sex, Grade, Date_of_Birth) " +
                        $" Values ('{StudentProfile.Student_ID}', '{StudentProfile.First_Name}', " +
                        $" '{StudentProfile.Middle_Name}', '{StudentProfile.Last_Name}', " +
                        $" '{StudentProfile.Sex}', '{StudentProfile.Grade}', " +
                        $" '{StudentProfile.Date_of_Birth}') ";

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