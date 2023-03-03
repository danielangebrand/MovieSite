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

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbM = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbM != null)
            {
                dbM.Name = data.Name;
                dbM.Descr = data.Descr;
                dbM.Price = data.Price;
                dbM.ImageURL = data.ImgURL;
                dbM.CinemaId = data.CinemaId;
                dbM.StartDate = data.StartDate;
                dbM.EndDate = data.EndDate;
                dbM.MovieCategory = data.MovieCategory;
                dbM.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }
            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie_Actors
            foreach (var actorId in data.ActorId)
            {
                var am = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(am);
            }
            await _context.SaveChangesAsync();
        }
    }
}
