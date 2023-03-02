using OnlineShop.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Descr { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
