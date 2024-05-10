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
    class ShowAllStudentsInfoRepo : BaseRepo
    {
        public bool ReadStudent (int studentID, StudentsModel studentsModel)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    string sql =
                        $" SELECT Student_ID, First_Name, Middle_Name, Last_Name, Sex, Grade, Date_of_Birth, Age " +
                        $" FROM Students " +
                        $" WHERE Student_Id = {studentID} ";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row1 = dt.Rows[0];

                        studentsModel.Student_ID = (int)row1[StudentsModel.fStudent_ID];
                        studentsModel.First_Name = (string)row1[StudentsModel.fFirst_Name];
                        studentsModel.Middle_Name = (string)row1[StudentsModel.fMiddle_Name];
                        studentsModel.Last_Name = (string)row1[StudentsModel.fLast_Name];
                        studentsModel.Sex = (string)row1[StudentsModel.fSex];
                        studentsModel.Grade = (string)row1[StudentsModel.fGrade];
                        studentsModel.Date_of_Birth = (DateTime)row1[StudentsModel.fDate_of_Birth];
                        studentsModel.Age = (int)row1[StudentsModel.fAge];
                    }
                    else
                    {
                        Console.WriteLine("No student record found");
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
                Console.WriteLine("Exception: " + e);
            }

            return success;
        }

        public bool ReadStudentSports(int studentID, List<SportsModel> SportsList) 
        {
            bool success = false;

            try
            {
                if(Connect())
                {
                    string sql =
                        $" SELECT Sports.Sport_ID, Sports.Sport_Gender, Sports.Sport_Name, Sports.Sport_Season " +
                        $" FROM Students " +
                        $" INNER JOIN Sports_Participation ON Students.Student_ID = Sports_Participation.Student_ID " +
                        $" INNER JOIN Sports ON Sports_Participation.Sport_ID = Sports.Sport_ID " +
                        $" WHERE Students.Student_ID = {studentID} ";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        SportsModel studentsportmodel;

                        foreach (DataRow row in dt.Rows)
                        {
                            studentsportmodel = new SportsModel()
                            {
                                Sport_Id = (int)row[SportsModel.fSport_Id],
                                Sport_Gender = (string)row[SportsModel.fSport_Gender],
                                Sport_Name = (string)row[SportsModel.fSport_Name],
                                Sport_Season = (string)row[SportsModel.fSport_Season],
                            };

                            SportsList.Add(studentsportmodel);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No student sports participation records found");
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
                Console.WriteLine("Exception:" + e);
            }

            return success;
        }

        public bool ReadStudentLibraries(int studentID, List<LibraryModel> LibraryList)
        {
            bool success = false;

            try
            {
                string sql =
                    $" SELECT Library_Books.Book_ID, Library_Books.Book_Title, Library_Books.Book_Author " +
                    $" FROM Students " +
                    $" INNER JOIN Library_Books on Students.Student_ID = Library_Books.Student_ID_Checked_Out " +
                    $" WHERE Students.Student_ID = {studentID}";

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sql, connection);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        LibraryModel librarymodel;

                        librarymodel = new LibraryModel()
                        {
                            Book_Id = (int)row[LibraryModel.fBook_Id],
                            Book_Title = (string)row[LibraryModel.fBook_Title],
                            Book_Author = (string)row[LibraryModel.fBook_Author]
                        };

                        LibraryList.Add(librarymodel);
                    }

                    success = true;
                }
                else
                {
                    Console.WriteLine("Failed to read student's library book(s) checked out");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to read students library for " + e);
            }

                return success;
        }
    }
}