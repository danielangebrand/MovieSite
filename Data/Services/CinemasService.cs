using OnlineShop.Data.Base;

namespace OnlineShop.Data.Services
{
    public class CinemasService:IEntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
            
        }
    }
}
