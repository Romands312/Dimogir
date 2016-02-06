using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dimogir.Web.Controllers
{
    public class ExerciseController : Controller
    {
        // GET: Exercise
        public ActionResult List()
        {
            return View();
        }
    }
}