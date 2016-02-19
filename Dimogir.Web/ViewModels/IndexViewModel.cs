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
            LessonLists = new LessonListViewModel[categories.Length];

            for (int i = 0; i < categories.Length; i++)
            {
                LessonLists[i] = new LessonListViewModel
                (
                    lessons.Where(les => les.CategoryId == categories[i].Id).ToArray(),
                    categories[i].Name
                );
            }
        }

        public CategoryListViewModel CategoryList { get; set; }

        public LessonListViewModel[] LessonLists { get; set; }
    }
}