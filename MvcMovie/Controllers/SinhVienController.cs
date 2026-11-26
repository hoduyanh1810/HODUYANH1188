using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Dễ thay đổi khi quy tắc cấp mã của trường thay đổi.
        private const string MaNamNhapHoc = "28";
        private const string MaHeDaoTao = "21";
        private const int SoThuTuDauTien = 199;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var danhSach = await _context.SinhVien
                .Include(s => s.Khoa)
                .OrderBy(s => s.MaSinhVien)
                .ToListAsync();

            return View(danhSach);
        }

        public async Task<IActionResult> Create()
        {
            await NapDanhSachKhoa();
            return View(new SinhVienCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SinhVienCreateViewModel model)
        {
            if (!await _context.Khoa.AnyAsync(k => k.MaKhoa == model.MaKhoa))
                ModelState.AddModelError(nameof(model.MaKhoa), "Khoa được chọn không tồn tại.");

            if (!ModelState.IsValid)
            {
                await NapDanhSachKhoa(model.MaKhoa);
                return View(model);
            }

            var maSinhVien = await TaoMaSinhVienAsync(model.MaKhoa);
            if (maSinhVien is null)
            {
                ModelState.AddModelError(string.Empty, "Không thể tạo mã sinh viên. Vui lòng kiểm tra mã khoa hoặc số thứ tự đã hết.");
                await NapDanhSachKhoa(model.MaKhoa);
                return View(model);
            }

            _context.SinhVien.Add(new SinhVien
            {
                MaSinhVien = maSinhVien,
                HoTen = model.HoTen.Trim(),
                MaKhoa = model.MaKhoa
            });

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Mã sinh viên vừa được cấp đã tồn tại. Vui lòng gửi lại biểu mẫu.");
                await NapDanhSachKhoa(model.MaKhoa);
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien is null)
                return NotFound();

            await NapDanhSachKhoa(sinhVien.MaKhoa);
            return View(new SinhVienEditViewModel
            {
                MaSinhVien = sinhVien.MaSinhVien,
                HoTen = sinhVien.HoTen,
                MaKhoa = sinhVien.MaKhoa
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SinhVienEditViewModel model)
        {
            if (id != model.MaSinhVien)
                return NotFound();

            if (!await _context.Khoa.AnyAsync(k => k.MaKhoa == model.MaKhoa))
                ModelState.AddModelError(nameof(model.MaKhoa), "Khoa được chọn không tồn tại.");

            if (!ModelState.IsValid)
            {
                await NapDanhSachKhoa(model.MaKhoa);
                return View(model);
            }

            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien is null)
                return NotFound();

            sinhVien.HoTen = model.HoTen.Trim();
            sinhVien.MaKhoa = model.MaKhoa;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.SinhVien.AnyAsync(s => s.MaSinhVien == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var sinhVien = await _context.SinhVien
                .Include(s => s.Khoa)
                .FirstOrDefaultAsync(s => s.MaSinhVien == id);

            return sinhVien is null ? NotFound() : View(sinhVien);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVien = await _context.SinhVien.FindAsync(id);
            if (sinhVien is not null)
            {
                _context.SinhVien.Remove(sinhVien);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<string?> TaoMaSinhVienAsync(string maKhoa)
        {
            // Quy ước bài tập dùng mã khoa 2 chữ số làm mã ngành.
            if (maKhoa.Length != 2 || !maKhoa.All(char.IsDigit))
                return null;

            var tienTo = MaNamNhapHoc + MaHeDaoTao + maKhoa;
            var maHienCo = await _context.SinhVien
                .AsNoTracking()
                .Where(s => s.MaSinhVien.StartsWith(tienTo))
                .Select(s => s.MaSinhVien)
                .ToListAsync();

            var soLonNhat = maHienCo
                .Where(ma => ma.Length == 10 && ma.All(char.IsDigit))
                .Select(ma => int.TryParse(ma.Substring(6, 4), NumberStyles.None,
                    CultureInfo.InvariantCulture, out var so) ? so : 0)
                .DefaultIfEmpty(SoThuTuDauTien - 1)
                .Max();

            var soTiepTheo = Math.Max(SoThuTuDauTien, soLonNhat + 1);
            if (soTiepTheo > 9999)
                return null;

            return tienTo + soTiepTheo.ToString("D4", CultureInfo.InvariantCulture);
        }

        private async Task NapDanhSachKhoa(string? maKhoaDaChon = null)
        {
            var khoas = await _context.Khoa
                .OrderBy(k => k.TenKhoa)
                .ToListAsync();

            ViewBag.MaKhoa = new SelectList(khoas, "MaKhoa", "TenKhoa", maKhoaDaChon);
        }
    }
}