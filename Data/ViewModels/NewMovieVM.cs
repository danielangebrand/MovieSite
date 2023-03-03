using OnlineShop.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description required")]
        public string Descr { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price required")]
        public double Price { get; set; }

        [Display(Name = "Movie poster URL")]
        [Required(ErrorMessage = "Movie poster URL ... IS REQUIRED! hahaha..")]
        public string ImgURL { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start date required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "End date required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Category is required")]
        public MovieCategory MovieCategory { get; set; }

        [Display(Name = "Select actor/actors")]
        [Required(ErrorMessage = "Movie actor(s) required!")]
        public List<int> ActorId { get; set; }

        [Display(Name = "Select cinema")]
        [Required(ErrorMessage = "Cinema required!")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required!")]
        public int ProducerId { get; set; }
    }
}
