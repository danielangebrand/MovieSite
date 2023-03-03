using OnlineShop.Data.Base;
using OnlineShop.Data.ViewModels;

namespace OnlineShop.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var m = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return m;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var r = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
            };
            return r;
        }

        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var m = new Movie()
            {
                Name = data.Name,
                Descr = data.Descr,
                Price = data.Price,
                ImageURL = data.ImgURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };

            await _context.Movies.AddAsync(m);
            await _context.SaveChangesAsync();

            foreach (var actorId in data.ActorId)
            {
                var am = new Actor_Movie()
                {
                    MovieId = m.Id,
                    ActorId = actorId
                };
            }
            await _context.SaveChangesAsync();
        }
    }
}
