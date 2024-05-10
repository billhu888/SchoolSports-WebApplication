using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using SchoolSports.Models;
using SchoolSports.Repositories;

namespace SchoolSports.Controllers
{
    public class FruitController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            FruitModel fruit = new FruitModel();
            FruitsRepo fr = new FruitsRepo();

            fruit.Fruits = new List<SelectListItem>();

            bool success = fr.ReadFruits(fruit.Fruits);

            if(success) 
            {
                return View(fruit);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(FruitModel SelectedFruits)
        {
            FruitsRepo fr = new FruitsRepo();
            SelectedFruits.Fruits = new List<SelectListItem>();

            bool success = fr.ReadFruits(SelectedFruits.Fruits);

            if (SelectedFruits.FruitIds != null)
            {
                List<SelectListItem> selectedItems = SelectedFruits.Fruits.Where
                    (p => SelectedFruits.FruitIds.Contains(int.Parse(p.Value))).ToList();

                ViewBag.Message = "Selected Fruits:";
                foreach (var selectedItem in selectedItems)
                {
                    selectedItem.Selected = true;
                    ViewBag.Message += "\\n" + selectedItem.Text;
                }
            }

            if (success)
            {
                return View(SelectedFruits);
            }
            else
            {
                return View();
            }
        }
    }
}