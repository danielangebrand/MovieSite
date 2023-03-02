using OnlineShop.Data.Base;

namespace OnlineShop.Data.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
            
        }
    }
}
