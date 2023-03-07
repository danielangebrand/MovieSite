namespace OnlineShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> item, string userId, string userEmail);
        Task<List<Order>> GetAllOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
