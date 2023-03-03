using OnlineShop.Data;

namespace OnlineShop.Models
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public List<ShoppingCartItem> GetShoppingCartItems() => ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());

        public double GetShoppingCartTotal() =>
            _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
            .Select(n => n.Movie.Price * n.Amount).Sum();

        public void AddItemToCart(Movie movie)
        {
            var item = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (item == null)
            {
                item = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
            }
            else item.Amount++;

            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie) 
        {
            var item = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (item == null) _context.ShoppingCartItems.Remove(item);
            else
            {
                if (item.Amount > 1)
                {
                    item.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(item);
                }
                _context.SaveChanges();
            }
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId};
        }
    }
}
