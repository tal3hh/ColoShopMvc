using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace DomainLayer.Entities
{
    public class ProductDetails : BaseEntity
    {
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
        public int Count { get; set; }
        public bool Availability { get; set; }
        public Reyting StarCount { get; set; }

        //Relation Property
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
