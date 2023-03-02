using OnlineShop.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile picture required")]
        public string ProfilePicURL { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "A name must have more than two letters")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }
        public List<Actor_Movie>? Actors_Movies { get; set; }
    }
}
