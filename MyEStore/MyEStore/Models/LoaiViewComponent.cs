using Microsoft.AspNetCore.Mvc;
using MyEStore.Entities;

namespace MyEStore.Models
{
    public class LoaiViewComponent : ViewComponent
    {
        private readonly MyeStoreContext _ctx;

        public LoaiViewComponent(MyeStoreContext ctx) {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke()
        {
            //gửi tới ~/Views/Shared/Components/Loai/Default.cshtml
            return View(_ctx.Loais.ToList());
        }
    }
}
