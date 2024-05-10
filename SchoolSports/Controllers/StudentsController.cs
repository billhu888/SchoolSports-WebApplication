using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolSports.Models;
using SchoolSports.Repositories;
using System.Diagnostics;

namespace SchoolSports.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddNewStudent()
        {
            StudentsModel NewStudent = new StudentsModel();
            AddNewStudentRepo NewStudentRepo = new AddNewStudentRepo();
            ViewData["isPost"] = false;

            bool success = NewStudentRepo.GetNewStudentID(NewStudent);

            if (success)
            {
                return View(NewStudent);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddNewStudent(StudentsModel NewStudent)
        {
            AddNewStudentRepo NewStudentRepo = new AddNewStudentRepo();
            ViewData["isPost"] = true;

            bool success = NewStudentRepo.AddNewStudent(NewStudent);

            ViewBag.Success = success;

            if (success)
            {
                return View(NewStudent);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult SearchStudentsResults(string FirstName, string LastName) 
        {
            MultipleStudentsModel msModel = new MultipleStudentsModel();
            StudentsRepo sRepo = new StudentsRepo();

            msModel.StudentsList = new List<StudentsModel>();

            bool success = sRepo.ReadStudents(msModel.StudentsList, FirstName, LastName);

            if (success) 
            {
                return View(msModel.StudentsList);
            }
            else
            {
                return View();
            }
        }

        public IActionResult ShowAllStudentInfo(int id)
        {
            ShowAllStudentInfoModel AllStudentInfoModel = new ShowAllStudentInfoModel();
            ShowAllStudentsInfoRepo AllStudentInfoRepo = new ShowAllStudentsInfoRepo();

            AllStudentInfoModel.StudentsModel = new StudentsModel();
            AllStudentInfoModel.ListSports = new List<SportsModel>();
            AllStudentInfoModel.ListLibrary = new List<LibraryModel>();

            bool success1 = AllStudentInfoRepo.ReadStudent(id, AllStudentInfoModel.StudentsModel);
            bool success2 = AllStudentInfoRepo.ReadStudentSports(id, AllStudentInfoModel.ListSports);
            bool success3 = AllStudentInfoRepo.ReadStudentLibraries(id, AllStudentInfoModel.ListLibrary);

            if (success1)
            {
                return View(AllStudentInfoModel);
            }
            else if (success1 && success2)
            {
                return View(AllStudentInfoModel);
            }
            else if (success1 && success3)
            {
                return View(AllStudentInfoModel);
            }
            else if (success1 && success2 && success3)
            {
                return View(AllStudentInfoModel);
            }
            else
            {
                return View();
            }
        }

        //public IActionResult AddStudentSportParticipation(int id, string sex)
        //{
        //    AddStudentSportParticipationModel sportsParModel = new AddStudentSportParticipationModel();
        //    AddStudentSportParticipationRepo AddSportPar = new AddStudentSportParticipationRepo();

        //    bool success = AddSportPar.ReadStudentSports(sportsParModel, id, sex);
        //    ViewData["isPost"] = false;

        //    return View(sportsParModel);
        //}

        //[HttpPost]
        //public IActionResult AddStudentSportParticipation(AddStudentSportParticipationModel AddSportsPar)
        //{
        //    AddStudentSportParticipationRepo AddSportPar = new AddStudentSportParticipationRepo();

        //    bool success = AddSportPar.AddSportsParticipationToDatabase(AddSportsPar);

        //    ViewData["isPost"] = true;
        //    ViewBag.Success = success;

        //    return View(AddSportsPar);
        //}

        public IActionResult AddStudentSportParticipation(int id, string sex)
        {
            AddStudentSportParticipationModel AddedSports = new AddStudentSportParticipationModel();
            AddStudentSportParticipationRepo AddedSportsRepo = new AddStudentSportParticipationRepo();
            ViewData["isPost"] = false;

            AddedSports.AddedSports = new List<SelectListItem>();
            AddedSports.StudentID = id;
            AddedSports.Sex = sex;

            bool success = AddedSportsRepo.ReadAddedSports(AddedSports.AddedSports, id, sex);

            if (success)
            {
                return View(AddedSports);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddStudentSportParticipation(AddStudentSportParticipationModel SelectedSports)
        {
            AddStudentSportParticipationRepo AddedSportsRepo = new AddStudentSportParticipationRepo();
            SelectedSports.AddedSports = new List<SelectListItem>();
            ViewData["isPost"] = true;
            ViewData["indicator"] = false;

            bool success = AddedSportsRepo.ReadAddedSports(SelectedSports.AddedSports, SelectedSports.StudentID, SelectedSports.Sex);

            if(SelectedSports.AddedSportsIds != null)
            {
                List<SelectListItem> ChosenSports = SelectedSports.AddedSports.Where
                    (p => SelectedSports.AddedSportsIds.Contains(int.Parse(p.Value))).ToList();

                foreach (var Sport in ChosenSports)
                {
                    Sport.Selected = true;
                }
            }

            bool success1 = AddedSportsRepo.AddInsertedSports(SelectedSports.AddedSportsIds, SelectedSports.StudentID);

            if (success && success1)
            {
                ViewData["indicator"] = true;

                return View(SelectedSports);
            }
            else
            {
                return View();
            }
        } 

        public IActionResult DeleteStudentSportsParticipation(int id, string sex)
        {
            DeleteStudentSportPParticipationModel DeleteSportsModel = new DeleteStudentSportPParticipationModel();
            DeleteStudentSportParticipationRepo DeleteSportsRepo = new DeleteStudentSportParticipationRepo();
            ViewData["isPost"] = false;

            DeleteSportsModel.DeletedSports = new List<SelectListItem>();
            DeleteSportsModel.StudentID = id;
            DeleteSportsModel.Sex = sex;

            bool success = DeleteSportsRepo.ReadDeletedSports(DeleteSportsModel.DeletedSports, id, sex);

            if (success)
            {
                return View(DeleteSportsModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteStudentSportsParticipation(DeleteStudentSportPParticipationModel RemovedSports)
        {
            DeleteStudentSportParticipationRepo DeletedSportsRepo = new DeleteStudentSportParticipationRepo();
            RemovedSports.DeletedSports = new List<SelectListItem>();
            ViewData["isPost"] = true;
            ViewData["indicator"] = false;

            bool success = DeletedSportsRepo.ReadDeletedSports(RemovedSports.DeletedSports, RemovedSports.StudentID, RemovedSports.Sex);

            if(RemovedSports.DeletedSportsIds != null)
            {
                List<SelectListItem> DeleteSports = RemovedSports.DeletedSports.Where
                    (p => RemovedSports.DeletedSportsIds.Contains(int.Parse(p.Value))).ToList();

                foreach (var Sport in DeleteSports)
                {
                    Sport.Selected = true;
                }
            }

            bool success1 = DeletedSportsRepo.DeleteSelectedSports(RemovedSports.DeletedSportsIds, RemovedSports.StudentID);

            if (success && success1)
            {
                ViewData["indicator"] = true;

                return View(RemovedSports);
            }
            else
            {
                return View();
            }
        }

        public IActionResult EditStudentProfile(int id)
        {
            StudentsModel StudentProfile = new StudentsModel();
            EditStudentProfileRepo EditStudentProfile = new EditStudentProfileRepo();
            bool success = EditStudentProfile.ReadStudent(StudentProfile, id);
            ViewData["isPost"] = false;

            if (success)
            {
                return View(StudentProfile);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult EditStudentProfile(StudentsModel StudentProfile)
        {
            EditedStudentProfileRepo EditedStudentProfile = new EditedStudentProfileRepo();
            bool success = EditedStudentProfile.UpdateStudent(StudentProfile);
            ViewData["isPost"] = true;

            ViewBag.Success = success;

            if (success)
            {
                return View(StudentProfile);
            }
            else
            {
                return View();
            }
        }

        //[HttpPost]
        //public IActionResult EditedStudentProfile(StudentsModel studentProfile)
        //{
        //    EditedStudentProfileRepo EditedStudentProfile = new EditedStudentProfileRepo();

        //    bool success = EditedStudentProfile.UpdateStudent(studentProfile);

        //    if (success)
        //    {
        //        return View(studentProfile);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        public IActionResult DeleteStudentProfile(int id)
        {
            StudentsModel StudentProfile = new StudentsModel();
            DeleteStudentProfileRepo DeleteStudentProfile = new DeleteStudentProfileRepo();

            bool success = DeleteStudentProfile.DeleteStudentProfile(StudentProfile, id);

            if (success)
            {
                return View(StudentProfile);
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}