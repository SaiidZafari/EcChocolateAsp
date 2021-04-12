using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcChocolateAsp.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        [Display(Name = "Number of guest")]
        public int Guest { get; set; }

        [Required]
        [Display(Name = "Arrangement Price")]
        public int ArrangementPrice { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
