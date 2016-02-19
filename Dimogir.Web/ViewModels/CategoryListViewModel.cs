using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dimogir.DomainModel;

namespace Dimogir.Web.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public string Key { get; set; }
    }

    public class CategoryListViewModel
    {
        public CategoryListViewModel()
        {    
        }

        public CategoryListViewModel(Category[] categories)
        {
            Categories = Mapper.Map<CategoryViewModel[]>(categories);
        }

        public CategoryViewModel [] Categories { get; set; }
    }
}