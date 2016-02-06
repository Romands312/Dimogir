using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dimogir.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("LessonList", "lesson/list", new { controller = "Lesson", action = "List" });

            routes.MapRoute("ShowLesson", "lesson/{id}", new { controller = "Lesson", action = "Show" });

            routes.MapRoute("ExerciseList", "exercise/list", new { controller = "Exercise", action = "List" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
