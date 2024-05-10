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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolSports.Repositories
{
    class DeleteStudentSportParticipationRepo : BaseRepo
    {
        public bool ReadDeletedSports(List<SelectListItem> DeletedSports, int StudentID, string Sex)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    String Query =
                        $" SELECT s.Sport_ID, s.Sport_Name " +
                        $" FROM SPORTS s " +
                        $" JOIN Sports_Participation sp " +
                        $" ON s.Sport_ID = sp.Sport_ID " +
                        $"    AND sp.Student_ID = '{StudentID}' ";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(Query, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            DeletedSports.Add(new SelectListItem
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

        public bool DeleteSelectedSports(int[] IDsDeletedSports, int IDStudent)
        {
            bool success = false;

            try
            {
                if (Connect())
                {
                    foreach (int SportID in IDsDeletedSports)
                    {
                        String sql =
                            $" DELETE " +
                            $" FROM SPORTS_PARTICIPATION " +
                            $" WHERE Student_ID = '{IDStudent}' " + 
                            $"    AND Sport_ID = '{SportID}' ";

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
    }
}