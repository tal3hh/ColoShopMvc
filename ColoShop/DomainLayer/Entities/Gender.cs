using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Gender : BaseEntity
    {
        public string Name { get; set; }

        //Relation Property

        public List<Product> Products { get; set; }
    }
}
