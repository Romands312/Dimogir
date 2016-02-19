using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dimogir.DomainModel;

namespace Dimogir.Web.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel(Category[] categories, Lesson[] lessons)
        {
            CategoryList = new CategoryListViewModel(categories);
            LessonLists = new List<LessonListViewModel>();

            foreach (Category category in categories)
            {
                var lessonList = new LessonListViewModel
                (
                    lessons.Where(les => les.CategoryId == category.Id).ToArray(),
                    category.Name
                );

                LessonLists.Add(lessonList);
            }
        }

        public CategoryListViewModel CategoryList { get; set; }

        public List<LessonListViewModel> LessonLists { get; set; }
    }
}