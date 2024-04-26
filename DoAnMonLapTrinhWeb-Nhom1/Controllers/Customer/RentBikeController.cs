using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Customer
{
    [Authorize(Roles = SD.Role_Customer)]   
    
    public class RentBikeController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;
        public RentBikeController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index(string bienSoXe)
        {
            DateTime Ngaynhan = DateTime.Now;
            DateTime Ngaytra = Ngaynhan.AddDays(1);
            string username = User.Identity.Name;
            YeuCauDatXe datXe = new YeuCauDatXe();
            datXe.NgayNhan = Ngaynhan;
            datXe.NgayTra = Ngaytra;
            List<SelectListItem> DanhGia = new List<SelectListItem>();
            List<SelectListItem> LoaiXe = new List<SelectListItem>();
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            foreach (var item in _context.DiaDiems)
            {
                DiaDiems.Add(new SelectListItem { Value = item.MaDiaDiem.ToString(), Text = item.TenDiaDiem });
            }
            foreach (var item in _context.LoaiXes)
            {
                LoaiXe.Add(new SelectListItem { Value = item.MaLoai.ToString(), Text = item.TenLoai });
            }
            var xe = await _context.Xes.FirstOrDefaultAsync(p => p.BienSoXe == bienSoXe);
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var xeList = await _context.Xes.Where(p => p.Email == username).ToListAsync();
            var viewmodel = new UserViewModel() {
                Xe = xe,
                datXe = datXe,
                datXeList = datxeList
            };
            return View(viewmodel);
        }

        public async Task<IActionResult> Menu()
        {
            return PartialView();
        }

        // Action để xử lý việc yêu cầu đặt của xe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(string bienSoXe, UserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return NotFound();
            }

            else
            {
                var users = new TaiKhoan();
                if (User.Identity.IsAuthenticated)
                {
                    string username = User.Identity.Name;
                    users = await _context.TaiKhoans.FirstOrDefaultAsync(m => m.Email == username);
                    if (username != null)
                    {
                        var existingRequests = await _context.YeuCauDatXes
                        .Where(x => x.BienSoXe == bienSoXe)
                        .ToListAsync();
                        var xe = await _context.Xes.FirstOrDefaultAsync(x => x.BienSoXe == bienSoXe);
                        if (xe != null)
                        {
                            // Kiểm tra xem người dùng có đang cố gắng đặt xe của bản thân hay không
                            if (xe.Email == users.Email)
                            {
                                TempData["ErrorMessage"] = "Không thể đặt xe của bản thân!";
                                return RedirectToAction("Index", "Home");
                            }
                            foreach (var request in existingRequests)
                            {
                                // Kiểm tra xem thời gian nhận xe của yêu cầu đặt xe mới có nằm trong khoảng thời gian của các yêu cầu đặt xe khác không
                                if (userViewModel.datXe.NgayNhan >= request.NgayNhan && userViewModel.datXe.NgayNhan <= request.NgayTra)
                                {
                                    // Nếu thời gian nhận xe nằm trong khoảng thời gian của một yêu cầu đặt xe khác, hiển thị thông báo lỗi và không lưu yêu cầu mới
                                    TempData["ErrorMessage"] = "Xe này đã có người đặt!";
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            userViewModel.datXe.BienSoXe = bienSoXe;

                            userViewModel.datXe.Email = users.Email;
                            // Tính số ngày thuê
                            DateTime ngayTra = userViewModel.datXe.NgayTra;
                            DateTime ngayNhan = userViewModel.datXe.NgayNhan;
                            TimeSpan thoiGianThue = ngayTra.Subtract(ngayNhan);
                            int soNgayThue = thoiGianThue.Days;

                            // Lưu số ngày thuê vào đối tượng
                            userViewModel.datXe.SoNgayThue = soNgayThue;

                            // Lưu yêu cầu đặt xe vào cơ sở dữ liệu
                            _context.YeuCauDatXes.Add(userViewModel.datXe);
                            await _context.SaveChangesAsync();
                            TempData["SuccessMessage"] = "Đặt xe thành công hãy chờ chủ xe xét duyệt!";
                            return RedirectToAction("Index", "Home");

                        }                    
                    }
                }
                // Gán giá trị Email cho đối tượng yeucaudatxe
                TempData["ErrorMessage"] = "Đặt xe thất bại!";
                return View();
            }
        }
		



	}
}
