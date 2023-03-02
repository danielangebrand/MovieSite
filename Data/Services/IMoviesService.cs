using OnlineShop.Data.Base;

namespace OnlineShop.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
    }
}
