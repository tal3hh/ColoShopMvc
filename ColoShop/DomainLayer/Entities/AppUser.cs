using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
