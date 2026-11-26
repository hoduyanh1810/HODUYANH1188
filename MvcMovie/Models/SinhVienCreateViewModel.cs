using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class SinhVienCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn khoa.")]
        public string MaKhoa { get; set; } = string.Empty;
    }
}