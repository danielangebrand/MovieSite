using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Services;
using OnlineShop.Data.Static;
using OnlineShop.Data.ViewModels;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null) _shoppingCart.RemoveItemFromCart(item);
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmail = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmail);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetAllOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
    }
}
