using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace ServiceLayer.DTOs.ProductDetails
{
    public class ProductDetailsCreateDto
    {
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
        public int Count { get; set; }
        public bool Availability { get; set; }
        public Reyting StarCount { get; set; }

        //Relation Property
        public int ProductID { get; set; }
    }
}
