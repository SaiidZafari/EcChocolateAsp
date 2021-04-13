using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcChocolateAsp.Models
{
    public class ReviewsDto
    {
        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Your Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Review Title")]
        public string ReviewTitle { get; set; }
        [Required]
        [Display(Name = "Review")]
        public string ReviewBody { get; set; }
        [Range(0,5)]
        [Display(Name = "Score")]
        public int ReviewScore { get; set; }
    }
}