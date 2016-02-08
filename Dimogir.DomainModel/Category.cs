using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dimogir.DomainModel
{
    public class Category : Entity<string>
    {
        public Category()
        {
            //Lessons= new List<Lesson>();
        }

        public string Name { get; set; }

        //public ICollection<Lesson> Lessons { get; set; }
    }
}
