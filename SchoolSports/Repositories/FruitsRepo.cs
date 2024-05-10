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
    class FruitsRepo : BaseRepo
    {
        public bool ReadFruits (List<SelectListItem> fruits)
        {
            bool success = true;

            try
            {
                if (Connect())
                {
                    string query = 
                        $" SELECT FruitName, FruitId " +
                        $" FROM Fruits";

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, connection);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            fruits.Add(new SelectListItem
                            {
                                Text = row["FruitName"].ToString(),
                                Value = row["FruitId"].ToString()
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
    }
}