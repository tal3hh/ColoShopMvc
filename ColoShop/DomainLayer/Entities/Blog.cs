using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DomainLayer.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string ByName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }

        //Relation Property

        public List<BlogComment> BlogComments { get; set; }

    }
}
