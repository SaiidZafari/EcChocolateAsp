using System.Collections.Generic;

namespace EcChocolateAsp.Areas.Admin.Models
{
    public class EditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
    }
}