﻿using Microsoft.AspNetCore.Mvc;
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
			var result = data.Select(hh => new HangHoaVM {
				MaHh = hh.MaHh, TenHh = hh.TenHh,
				DonGia = hh.DonGia ?? 0, Hinh = hh.Hinh
			}).ToList();
			return View(result);
		}
	}
}