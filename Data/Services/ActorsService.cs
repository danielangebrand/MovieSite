using Microsoft.Identity.Client;
using OnlineShop.Data.Base;

namespace OnlineShop.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context)
        {
        }
    }
}
