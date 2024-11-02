using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyEStore.Entities;
using MyEStore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MyEStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MyeStoreContext _ctx;
        public CustomerController(MyeStoreContext ctx) {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, string ReturnUrl = null)
        {
            var khachHang = _ctx.KhachHangs.SingleOrDefault(p => p.MaKh == model.UserName && p.MatKhau == model.Password);
            ViewBag.ReturnUrl = ReturnUrl;
            if (khachHang == null)
            {
                TempData["ThongBao"] = "Sai thông tin đăng nhập";
                return View();
            }

            //Khai báo claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, khachHang.Email),
                new Claim(ClaimTypes.Name, khachHang.HoTen),
                new Claim("ID", khachHang.MaKh),
                //quyền (role)
                new Claim(ClaimTypes.Role, "Administartor"),
                new Claim(ClaimTypes.Role, "Accountant"),
            };
            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            await HttpContext.SignInAsync(claimPrincipal);

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Profile", "Customer");
        }

        [Authorize]
        public IActionResult PurchaseHistory()
        {
            return View();
        }


        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
