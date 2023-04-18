using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class ProductComment: BaseEntity
    {
        public string FullName { get; set; }
        public string Message { get; set; }
        public int ProductID { get; set; }

        //Relation Property
        public Product Product { get; set; }
        

    }
}
