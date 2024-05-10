using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolSports.Models
{
    public class DeleteStudentSportPParticipationModel
    {
        public int StudentID { get; set; }
        public string Sex { get; set; }
        public List<SelectListItem> DeletedSports { get; set; }
        public int[] DeletedSportsIds { get; set; }
    }
}
