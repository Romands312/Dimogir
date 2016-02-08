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

        public LessonController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Lesson
        public ActionResult Show()
        {
            return View();
        }

        public ActionResult List()
        {
            Category[] categories = _categoryService.GetAll();

            var categoryListViewModel = new CategoryListViewModel();
            categoryListViewModel.CategoryViewModels = Mapper.Map<CategoryViewModel[]>(categories);

            return View(categoryListViewModel);
        }
    }
}