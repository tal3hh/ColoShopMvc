using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace DomainLayer.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }       
        public decimal Price { get; set; }
        public Tickets Ticket { get; set; }        
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }


        //Relation Property
        public int CategoryID { get; set; }
        public int GenderID { get; set; }
        public Category Category { get; set; }
        public Gender Gender { get; set; }
        public ProductDetails ProductDetails { get; set; }
        public List<ProductComment> ProductComments { get; set; }
    }
}
