using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolSports.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchoolSports.Repositories
{
    class StudentsRepo : BaseRepo
    {
        public bool ReadStudents(List<StudentsModel> StudentsList, string fName, string lName)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    string sql =
                        $" SELECT Student_ID, First_Name, Middle_Name, Last_Name " +
                        $" FROM Students";

                    if (string.IsNullOrEmpty(fName))
                    {
                        sql = sql + $" WHERE Last_Name = '{lName}'";
                    }
                    else
                    {
                        sql = sql + $" WHERE First_Name = '{fName}' " +
                            $" AND Last_Name = '{lName}'";
                    }

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            StudentsModel student = new StudentsModel
                            {
                                Student_ID = Convert.ToInt32(row[StudentsModel.fStudent_ID]),
                                First_Name = row[StudentsModel.fFirst_Name].ToString(),
                                Middle_Name = row[StudentsModel.fMiddle_Name].ToString(),
                                Last_Name = row[StudentsModel.fLast_Name].ToString()
                            };

                            StudentsList.Add(student);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }

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