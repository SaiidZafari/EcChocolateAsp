using EcChocolateAsp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Areas.Admin.Models
{
    public class ProductImagesViewModel
    {
        public int Id { get; set; }
        public ICollection<ImageUrl> Images { get; set; }
        public ICollection<string> NewImages { get; set; }
        public string TextAreaData { get; set; }
    }
}
