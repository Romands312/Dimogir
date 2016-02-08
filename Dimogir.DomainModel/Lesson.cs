using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimogir.DomainModel
{
    public class Lesson : Entity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public int ParentId { get; set; }
    }
}
