using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dimogir.Web.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public string Key { get; set; }
    }

    public class CategoryListViewModel
    {
        public CategoryViewModel [] CategoryViewModels { get; set; }
    }
}