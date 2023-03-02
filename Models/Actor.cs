using OnlineShop.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Pic")]
        [Required(ErrorMessage = "Profile picture required!")]
        public string ProfilePicURL { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Name required!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be more than two characters.. " )]
        public string FullName { get; set; }
        [Display(Name = "Biography")]

        public string Bio { get; set; }
        public List<Actor_Movie>? Actors_Movies { get; set; }
    }
}
