using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSports.Models
{
    public class AddStudentSportParticipationModel
    {
        //public List<SportsModel> ListSports { get; set; }
        //public List<SportsParticipationModel> ListSportsParticipation { get; set; }

        public int StudentID { get; set; }
        public string Sex { get; set; }
        public List<SelectListItem> AddedSports { get; set; }
        public int[] AddedSportsIds { get; set; }
    }
}