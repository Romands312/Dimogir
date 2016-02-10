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
    public class LessonController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILessonService _lessonService;

        public LessonController(ICategoryService categoryService,
            ILessonService lessonService)
        {
            _categoryService = categoryService;
            _lessonService = lessonService;
        }
        
        public ActionResult CategoryList()
        {
            Category[] categories = _categoryService.GetAll();

            var categoryListViewModel = new CategoryListViewModel();
            categoryListViewModel.CategoryViewModels = Mapper.Map<CategoryViewModel[]>(categories);

            return View("CategoryList", categoryListViewModel);
        }

        public ActionResult List(string categoryKey)
        {
            var category = _categoryService.Load(categoryKey);
            if(category == null)
                throw new InvalidOperationException("Category with key " + categoryKey + " not found.");

            Lesson[] lessons = _lessonService.Find(categoryKey);

            var lessonListViewModel = new LessonListViewModel(lessons);
            lessonListViewModel.CategoryName = category.Name;

            return View("List", lessonListViewModel);
        }

        public ActionResult Show(string lessonKey)
        {
            Lesson lesson = _lessonService.Load(Convert.ToInt32(lessonKey));
            LessonViewModel lessonViewModel = Mapper.Map<LessonViewModel>(lesson);

            return View("Show", lessonViewModel);
        }
    }
}