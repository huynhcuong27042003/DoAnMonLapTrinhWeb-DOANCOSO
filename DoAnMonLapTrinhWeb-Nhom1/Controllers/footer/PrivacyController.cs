using Microsoft.AspNetCore.Mvc;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.footer
{
    public class PrivacyController : Controller
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
