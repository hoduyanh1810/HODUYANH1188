using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Khoa
    {
        [Key]
        [Required(ErrorMessage = "Vui lòng nhập mã khoa.")]
        [StringLength(2, ErrorMessage = "Mã khoa tối đa 2 ký tự.")]
        public string MaKhoa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên khoa.")]
        [StringLength(100, ErrorMessage = "Tên khoa tối đa 100 ký tự.")]
        public string TenKhoa { get; set; } = string.Empty;

        public ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
    }
}