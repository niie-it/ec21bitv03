using Microsoft.AspNetCore.Mvc;
using MyEStore.Entities;
using MyEStore.Models;

namespace MyEStore.Controllers
{
    public class CartController : Controller
    {
        private readonly MyeStoreContext _ctx;

        public CartController(MyeStoreContext ctx)
        {
            _ctx = ctx;
        }

        public static string CART_KEY = "CART";
        public List<CartItem> CartItems
        {
            get
            {
                return HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
            }
            set { HttpContext.Session.Set(CART_KEY, value); }
        }

        public IActionResult AddToCart(int id, int qty = 1)
        {
            var cart = CartItems;
            var item = cart.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                item.SoLuong += qty;
            }
            else
            {
                var hangHoa = _ctx.HangHoas.SingleOrDefault(p => p.MaHh == id);
                if (hangHoa == null) { return NotFound(); }
                item = new CartItem
                {
                    MaHh = id,
                    SoLuong = qty,
                    TenHh = hangHoa.TenHh,
                    DonGia = hangHoa.DonGia ?? 0,
                    Hinh = hangHoa.Hinh
                };
                cart.Add(item);
            }
            CartItems = cart;
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(CartItems);
        }
        
        public IActionResult RemoveCartItem(int id)
        {
            var cart = CartItems;
            var item = cart.SingleOrDefault(p => p.MaHh == id);
            if(item != null)
            {
                cart.Remove(item);
                CartItems = cart;//update session
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            CartItems = new List<CartItem>();
            return RedirectToAction("Index");
        }
    }
}
