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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolSports.Repositories
{
    class AddStudentSportParticipationRepo : BaseRepo
    {
        public bool ReadAddedSports(List<SelectListItem> AddedSports, int StudentID, string sex)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    string gender;

                    if (sex == "Male")
                    {
                        gender = "Girls";
                    }
                    else
                    {
                        gender = "Boys";
                    }

                    string Query =
                        $" SELECT s.Sport_ID, s.Sport_Name " +
                        $" FROM SPORTS s " +
                        $" LEFT JOIN Sports_Participation sp " +
                        $" ON s.Sport_ID = sp.Sport_ID " +
                        $"    AND sp.Student_ID = '{StudentID}' " +
                        $" WHERE sp.Student_ID IS NULL " +
                        $"    AND s.Sport_Gender != '{gender}'";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(Query, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            AddedSports.Add(new SelectListItem
                            {
                                Text = row["Sport_Name"].ToString(),
                                Value = row["Sport_ID"].ToString()
                            });
                        }
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

        public bool AddInsertedSports(int[] IDsAddedSports, int IDStudent)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    foreach (int SportID in IDsAddedSports)
                    {
                        int MaxParID = 0;

                        String sqlGetMaxId =
                            $" SELECT MAX(Participation_ID) AS MaxParID  " +
                            $" FROM SPORTS_PARTICIPATION ";

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(sqlGetMaxId, connection);
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow row1 = dt.Rows[0];

                            MaxParID = (int)row1["MaxParID"] + 1;
                        }

                        String sql =
                            $" INSERT INTO SPORTS_PARTICIPATION " +
                            $" (Participation_ID, Sport_ID, Student_ID) " +
                            $" VALUES ('{MaxParID}', '{SportID}', '{IDStudent}') ";

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
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

        //public bool ReadStudentSports(AddStudentSportParticipationModel NewSportsPar, int StudentID, string sex)
        //{
        //    bool success = false;

        //    NewSportsPar.StudentID = StudentID;
        //    NewSportsPar.Sex = sex;
        //    NewSportsPar.ListSports = new List<SportsModel>();

        //    try
        //    {
        //        if (Connect())
        //        {
        //            string gender;

        //            if (sex == "Male")
        //            {
        //                gender = "Girls";
        //            }
        //            else
        //            {
        //                gender = "Boys";
        //            }

        //            string sql =
        //                $" SELECT s.Sport_ID, s.Sport_Gender, s.Sport_Name, s.Sport_Season " +
        //                $" FROM SPORTS s " +
        //                $" LEFT JOIN Sports_Participation sp " +
        //                $" ON s.Sport_ID = sp.Sport_ID " +
        //                $"    AND sp.Student_ID = '{StudentID}' " +
        //                $" WHERE sp.Student_ID IS NULL " +
        //                $"    AND s.Sport_Gender != '{gender}' ";

        //            DataTable dt = new DataTable();
        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = new SqlCommand(sql, connection);
        //            da.Fill(dt);

        //            if (dt.Rows.Count > 0)
        //            {
        //                SportsModel sportmodel;

        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    sportmodel = new SportsModel()
        //                    {
        //                        Sport_Id = (int)row[SportsModel.fSport_Id],
        //                        Sport_Gender = (string)row[SportsModel.fSport_Gender],
        //                        Sport_Name = (string)row[SportsModel.fSport_Name],
        //                        Sport_Season = (string)row[SportsModel.fSport_Season],
        //                    };

        //                    NewSportsPar.ListSports.Add(sportmodel);
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("No sports found");
        //            }

        //            success = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to connect to database");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Exception: " + e);
        //    }

        //    return success;
        //}

        //public bool AddSportsParticipationToDatabase(AddStudentSportParticipationModel AddSportsPar)
        //{
        //    bool success = false;
        //    int StudentID = AddSportsPar.StudentID;
        //    List<int> selectedSportIds = AddSportsPar.ListSports.Select(s => s.Sport_Id).ToList();

        //    try
        //    {
        //        if (Connect())
        //        {
        //            foreach (int SportID in selectedSportIds) 
        //            {
        //                String sqlGetMaxId =
        //                    $" SELECT MAX(Participation_ID) AS MaxParID  " +
        //                    $" FROM SPORTS_PARTICIPATION ";

        //                int MaxParID = int.Parse(sqlGetMaxId) + 1;

        //                String sql =
        //                    $" INSERT INTO SPORTS_PARTICIPATION " +
        //                    $" (Participation_ID, Sport_ID, Student_ID) " +
        //                    $" VALUES ('{MaxParID}', '{SportID}', '{StudentID}' ";
        //            }

        //            success = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to connect to database");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Exception: " + e);
        //    }

        //    return success;
        //}
    }
}