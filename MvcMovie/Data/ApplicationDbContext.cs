using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Khoa> Khoa { get; set; }
        public DbSet<SinhVien> SinhVien { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.Khoa)
                .WithMany(k => k.SinhViens)
                .HasForeignKey(s => s.MaKhoa)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Khoa>().HasData(
                new Khoa { MaKhoa = "05", TenKhoa = "Công nghệ thông tin" },
                new Khoa { MaKhoa = "01", TenKhoa = "Dầu khí" },
                new Khoa { MaKhoa = "02", TenKhoa = "Trắc địa - Bản đồ và Thông tin địa lý" },
                new Khoa { MaKhoa = "03", TenKhoa = "Mỏ" }
            );
        }
    }
}