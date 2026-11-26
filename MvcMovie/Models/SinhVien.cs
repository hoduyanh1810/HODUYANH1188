using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class SinhVien
    {
        [Key]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mã sinh viên phải gồm đúng 10 ký tự.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mã sinh viên phải gồm đúng 10 chữ số.")]
        public string MaSinhVien { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn khoa.")]
        [StringLength(2)]
        public string MaKhoa { get; set; } = string.Empty;

        [ForeignKey(nameof(MaKhoa))]
        public Khoa? Khoa { get; set; }
    }
}