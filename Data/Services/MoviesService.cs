using OnlineShop.Data.Base;

namespace OnlineShop.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        public MoviesService(AppDbContext context) : base(context)
        {
            
        }
    }
}
