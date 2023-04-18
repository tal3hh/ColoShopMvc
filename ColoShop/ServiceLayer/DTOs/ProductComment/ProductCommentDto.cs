using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.ProductComment
{
    public class ProductCommentDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public int ProductID { get; set; }
    }
}
