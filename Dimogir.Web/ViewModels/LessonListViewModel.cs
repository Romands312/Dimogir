using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dimogir.DomainModel;

namespace Dimogir.Web.ViewModels
{
    public class LessonViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public string Key { get; set; }
    }

    public class LessonListViewModel
    {
        public LessonListViewModel()
        {
        }

        public LessonListViewModel(Lesson[] lessons, string categoryName)
        {
            Lessons = Mapper.Map<LessonViewModel[]>(lessons);
            CategoryName = categoryName;
        }

        public LessonViewModel [] Lessons { get; set; }
        public string CategoryName { get; set; }
    }
}