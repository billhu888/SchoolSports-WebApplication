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
    class DeleteStudentProfileRepo : BaseRepo
    {
        public bool DeleteStudentProfile(StudentsModel Student, int id)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    string sql =
                        $" SELECT Students.Student_ID, Students.First_Name, Students.Middle_Name, " +
                        $" Students.Last_Name, Students.Sex, Students.Grade, Students.Date_of_Birth " +
                        $" FROM STUDENTS " +
                        $" WHERE Student_ID = {id} ";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row1 = dt.Rows[0];

                        Student.Student_ID = (int)row1[StudentsModel.fStudent_ID];
                        Student.First_Name = (string)row1[StudentsModel.fFirst_Name];
                        Student.Middle_Name = (string)row1[StudentsModel.fMiddle_Name];
                        Student.Last_Name = (string)row1[StudentsModel.fLast_Name];
                        Student.Sex = (string)row1[StudentsModel.fSex];
                        Student.Grade = (string)row1[StudentsModel.fGrade];
                        Student.Date_of_Birth = (DateTime)row1[StudentsModel.fDate_of_Birth];
                    }
                    else
                    {
                        Console.WriteLine("No student sports participation records found");
                    }

                    sql =
                        $" DELETE FROM STUDENTS " +
                        $" WHERE Student_ID = {id} ";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("No student sports participation records found");
                }

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return success;
        }
    }
}