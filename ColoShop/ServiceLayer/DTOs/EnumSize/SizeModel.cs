using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace ServiceLayer.DTOs.EnumSize
{
    public class SizeModel
    {
        public Sizes Size { get; set; }
        public bool IsSelected { get; set; }
    }
}
