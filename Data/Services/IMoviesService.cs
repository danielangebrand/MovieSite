using OnlineShop.Data.Base;
using OnlineShop.Data.ViewModels;

namespace OnlineShop.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM m);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
