using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Areas.Admin.ViewModels
{
    public class EditUserViewModel
    {

        //It is solution for NullExeption (
        //the Users is a colation property and the collation is not initialized
        //the user is null and due to using Any() method in EditRole.cshtml we get Exception
        //therefor we incloud the structuer and initialized the property)
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //public string City { get; set; }

        public List<string> Roles { get; set; }

        public List<string> Claims { get; set; }
    }
}
