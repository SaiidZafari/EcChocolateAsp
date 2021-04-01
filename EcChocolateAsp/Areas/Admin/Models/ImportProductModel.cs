using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Areas.Admin.Models
{
    public class ImportProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int TotalQty { get; set; }
        public string Description { get; set; }
        public string Image_path_mobile { get; set; }
        public string Image_path_tablet { get; set; }
        public string Image_path_desktop { get; set; }
        public IList<string> Images { get; set; } = new List<string>();
    }
}
