using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolSports.Models
{
    public class FruitModel
    {
        public List<SelectListItem> Fruits { get; set; }
        public int[] FruitIds { get; set; }
    }
}