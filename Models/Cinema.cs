using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
