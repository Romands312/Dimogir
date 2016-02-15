using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Dimogir.DomainModel;
using Dimogir.Services;
using Dimogir.Web.ViewModels;

namespace Dimogir.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly ICategoryService _categoryService;

        public AdminController(ILessonService lessonService,
            ICategoryService categoryService)
        {
            _lessonService = lessonService;
            _categoryService = categoryService;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditCategory()
        {
            CategoryEditViewModel categoryEditViewModel = new CategoryEditViewModel();
            return View(categoryEditViewModel);
        }

        [HttpPost]
        [ActionName("AddCategory")]
        public ActionResult CreateOrUpdateCategory(CategoryEditViewModel categoryEditViewModel)
        {
            Category category = Mapper.Map<Category>(categoryEditViewModel);
            _categoryService.Create(category);
            return RedirectToAction("Index");
        }

        public ActionResult EditLesson()
        {
            LessonEditViewModel lessonEditViewModel = new LessonEditViewModel();
            lessonEditViewModel.Lessons = Mapper.Map<LessonViewModel[]>(_lessonService.GetOrderedByTitle());
            lessonEditViewModel.Categories = Mapper.Map<CategoryViewModel[]>(_categoryService.GetAll());
            return View(lessonEditViewModel);
        }

        [HttpPost]
        public ActionResult CreateOrUpdateLesson(LessonEditViewModel lessonEditViewModel)
        {
            Lesson lesson = Mapper.Map<Lesson>(lessonEditViewModel);
            _lessonService.Create(lesson);
            return RedirectToAction("Index");
        }
    }
}