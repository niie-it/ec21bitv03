using Microsoft.AspNetCore.Mvc;
using MyEStore.Entities;
using MyEStore.Models;

namespace MyEStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyeStoreContext _ctx;

        public ProductsController(MyeStoreContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Index(int? cateId)
        {
            var data = _ctx.HangHoas.AsQueryable();
            if (cateId.HasValue)
            {
                data = data.Where(hh => hh.MaLoai == cateId.Value);
            }
            var result = data.Select(hh => new HangHoaVM
            {
                MaHh = hh.MaHh,
                TenHh = hh.TenHh,
                TenAlias = hh.TenAlias,
                DonGia = hh.DonGia ?? 0,
                Hinh = hh.Hinh
            }).ToList();
            return View(result);
        }

        [HttpGet("san-pham/{slug}")]
		public IActionResult MoreDetail(string slug)
		{
			var data = _ctx.HangHoas.SingleOrDefault(hh => hh.TenAlias == slug);
			if (data == null)
			{
				return NotFound();
			}
			return View("Detail", data);
		}

		public IActionResult Detail(int id)
        {
            var data = _ctx.HangHoas.SingleOrDefault(hh => hh.MaHh == id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
    }
}
