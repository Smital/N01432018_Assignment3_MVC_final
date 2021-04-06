using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N01432018_Assignment3_MVC.Models;

namespace N01432018_Assignment3_MVC.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : Teacher(name of Controller)/List
        public ActionResult List(string SearchKey,string Number)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey,Number);

            return View(Teachers);
        }
        //GET : Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher newTeacher = controller.findTeacher(id);
            return View(newTeacher);
        }

    }
}
