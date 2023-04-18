using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Tickets Ticket { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

        public Colors Color { get; set; }
        public Sizes Size { get; set; }
        public int Count { get; set; }
        public bool Availability { get; set; }
        public Reyting StarCount { get; set; }

        public string CategoryName { get; set; }
        public string GenderName { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int GenderID { get; set; }
    }
}
