using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class BlogComment : BaseEntity
    {
        public string FullName { get; set; }
        public string Message { get; set; }
        public int BlogID { get; set; }

        //Relation Property
        public Blog Blog { get; set; }
        
    }
}
