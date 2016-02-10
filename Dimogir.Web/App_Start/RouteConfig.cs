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

            routes.MapRoute("LessonList", "lesson/{categoryKey}/list", new { controller = "Lesson", action = "List" });

            routes.MapRoute("CategoryList", "lesson/categories", new { controller = "Lesson", action = "CategoryList" });

            routes.MapRoute("ShowLesson", "lesson/{lessonKey}", new { controller = "Lesson", action = "Show" });

            routes.MapRoute("ExerciseList", "exercise/list", new { controller = "Exercise", action = "List" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
