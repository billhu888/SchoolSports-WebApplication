using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolSports.Models;
using SchoolSports.Repositories;
using System.Diagnostics;

namespace SchoolSports.Controllers
{
    ///
    public class StudentsController : Controller
    {
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     GET: /Students
        ///     Allows you to look up students by entering at least a last name
        ///        but can also enter a first name to be more specific
        ///     Can add a new student as well
        /// </summary>
        /// 
        /// <returns>
        ///     A page where you can search up students by entering at least a
        ///        last name but can also enter a first name to be more specific
        ///     Can also click on a link to add a new student
        /// </returns>

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     GET: /Students/AddNewStudent
        ///     You can add a new student to the database by entering the 
        ///        student's information
        /// </summary>
        /// 
        /// <returns>
        ///     A form where you can enter a new student by entering and 
        ///        submitting the new student's information
        /// </returns>

        public IActionResult AddNewStudent()
        {
            StudentsModel NewStudent = new StudentsModel();
            AddNewStudentRepo NewStudentRepo = new AddNewStudentRepo();
            ViewData["isPost"] = false;

            // Get the ID for the new student
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

        /// <summary>
        ///     POST: /Students/AddNewStudent
        ///     Adds a new student's information to the database
        /// </summary>
        /// 
        /// <param name="NewStudent">
        ///     A model that holds the new student's information
        /// </param>
        /// 
        /// <returns>
        ///     A page confirming that adding the new student to the 
        ///        database has been successful
        /// </returns>

        [HttpPost]
        public IActionResult AddNewStudent(StudentsModel NewStudent)
        {
            AddNewStudentRepo NewStudentRepo = new AddNewStudentRepo();
            ViewData["isPost"] = true;

            // Adds the new student to the database
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

        /// <summary>
        ///     
        /// </summary>
        /// 
        /// <param name="FirstName">
        /// 
        /// </param>
        /// 
        /// <param name="LastName">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>  
        /// 
        /// <param name="id">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="id">
        /// 
        /// </param>
        /// 
        /// <param name="sex">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="SelectedSports">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="id">
        /// 
        /// </param>
        /// 
        /// <param name="sex">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="RemovedSports">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="id">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="StudentProfile">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="id">
        /// 
        /// </param>
        /// 
        /// <returns>
        /// 
        /// </returns>

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