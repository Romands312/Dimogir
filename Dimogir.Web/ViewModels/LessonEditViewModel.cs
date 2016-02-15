using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dimogir.Web.ViewModels
{
    public class LessonEditViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public int? ParentId { get; set; }

        public string CategoryId { get; set; }

        public LessonViewModel[] Lessons { get; set; }

        public CategoryViewModel[] Categories { get; set; }
    }
}