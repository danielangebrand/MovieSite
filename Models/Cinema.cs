using OnlineShop.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Logo required")]
        public string Logo { get; set; }


        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "A Cinema must have a name..")]
        public string Name { get; set; }



        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please provide a description")]
        public string Descr { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
