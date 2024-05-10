using Microsoft.AspNetCore.Mvc;

namespace SchoolSports.Models
{
    public class SportsParticipationModel
    {
        public int Participation_ID { get; set; }
        public int Sport_ID { get; set; }
        public int Student_ID { get; set; }
    }
}
