using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Pic URL")]
        public string ProfilePicURL { get; set; }
        [Display(Name = "Full Name")]

        public string FullName { get; set; }
        [Display(Name = "Biography")]

        public string Bio { get; set; }
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
