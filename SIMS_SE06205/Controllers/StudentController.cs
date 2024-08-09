using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SIMS_SE06205.Models;

namespace SIMS_SE06205.Controllers
{
    public class StudentController : Controller
    {
        private string filePathStudent = @"D:\DDD\APDP-BTec-main\data-sims\\data-student.json";
        [HttpGet]
        public IActionResult Index()
        {
            string dataJson = System.IO.File.ReadAllText(filePathStudent);
            StudentModel model = new StudentModel();
            model.StudentLists = new List<StudentViewModel>();
            var students = JsonConvert.DeserializeObject<List<StudentViewModel>>(dataJson);
            var dataStudents = (from s in students select s).ToList();
            foreach (var item in dataStudents)
            {
                model.StudentLists.Add(new StudentViewModel
                {
                    Id = item.Id,
                    Code = item.Code,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Address = item.Address,
                    Phone = item.Phone,
                    Gender = item.Gender,
                    BirthDay = item.BirthDay,

                });

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            StudentViewModel model = new StudentViewModel();
            return View();
        }
        [HttpPost]
        public IActionResult Add(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                //bao loi
                try
                {
                    string dataJson = System.IO.File.ReadAllText(filePathStudent);
                    var students = JsonConvert.DeserializeObject<List<StudentViewModel>>(dataJson);
                    int maxId = 0;
                    if (students != null)
                    {
                        maxId = int.Parse((from s in students select s.Id).Max()) + 1;
                    }
                    string idIncrement = maxId.ToString();
                    students.Add(new StudentViewModel
                    {
                        Id = idIncrement,
                        Code = model.Code,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                        
                    Phone = model.Phone,
                    Gender = model.Gender,
                        
                    BirthDay = model.BirthDay,

                    });
                    var dtJson = JsonConvert.SerializeObject(students, Formatting.Indented);
                    System.IO.File.WriteAllText(filePathStudent, dtJson);
                    TempData["saveStatus"] = true; 
                }

                catch
                {
                    TempData["saveStatus"] = false;
                }//quay ve trang list
                return RedirectToAction(nameof(StudentController.Index), "Student");
            }
            return View(model);
        }
        //phan nay bat dau lam xoa
     

    }
}
