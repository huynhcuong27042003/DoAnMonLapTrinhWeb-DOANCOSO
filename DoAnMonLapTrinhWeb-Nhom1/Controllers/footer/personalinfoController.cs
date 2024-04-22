using Microsoft.AspNetCore.Mvc;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.footer
{
    public class personalinfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Menu()
        {

            return PartialView();
        }
    }
}
