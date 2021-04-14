using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Areas.Admin.ViewModels
{
    public class EditRoleViewModel
    {
        //It is solution for NullExeption (
        //the Users is a colation property and the collation is not initialized
        //the user is null and due to using Any() method in EditRole.cshtml we get Exception
        //therefor we incloud the structuer and initialized the property)
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }


        public string Id { get; set; }

        [Required (ErrorMessage ="RoleName is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
