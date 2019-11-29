using System;
using System.Linq.Expressions;
using ClientNotifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Models;
using RepositoryDemo.Repository;
using static ClientNotifications.Helpers.NotificationHelper;

namespace RepositoryDemo.Controllers
{
    public class StudentController : BaseController<Student>

    {
        private readonly IStudentRepository _studentrepository;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentController(IStudentRepository studentrepository,
                                    IClientNotification clientNotification,
                                    UserManager<IdentityUser> userManager)

            : base(clientNotification)
        {
            _studentrepository = studentrepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var student = _studentrepository.GetAll();
            // var prds = _studentrepository.GetBy(x=>x.Name == "abc" && x.Id == 1);

            return View(student);
        }

        public IActionResult Detail(int id)
        {
            var st = _studentrepository.GetSingle(x => x.Id == id);
            return View(st);
        }

        [HttpPost]
        public IActionResult Create(Student st, IFormFile file)
        {
           st.ImageUrl = UploadFile(file);

            base.Create(_studentrepository, st, st.Name);

            var userId = GetUserId(_userManager);

            return RedirectToAction(nameof(Index));

        }
        public IActionResult New()
        {
            return View();
        }


        public IActionResult Delete(int id){
            Remove(_studentrepository,x=>x.Id == id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
             var student = _studentrepository.GetSingle(x=>x.Id==id);
           
            return View(student);

        } 
[HttpPost]
        public IActionResult Update(Student st )
        {
            base.Update(_studentrepository,st,"Student");
            return RedirectToAction("Index");

        }


    }
}