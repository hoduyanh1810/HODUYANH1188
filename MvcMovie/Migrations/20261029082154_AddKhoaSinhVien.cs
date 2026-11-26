using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class AddKhoaSinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    MaKhoa = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    TenKhoa = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.MaKhoa);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    MaSinhVien = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MaKhoa = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.MaSinhVien);
                    table.ForeignKey(
                        name: "FK_SinhVien_Khoa_MaKhoa",
                        column: x => x.MaKhoa,
                        principalTable: "Khoa",
                        principalColumn: "MaKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Khoa",
                columns: new[] { "MaKhoa", "TenKhoa" },
                values: new object[,]
                {
                    { "01", "Dầu khí" },
                    { "02", "Trắc địa - Bản đồ và Thông tin địa lý" },
                    { "03", "Mỏ" },
                    { "05", "Công nghệ thông tin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaKhoa",
                table: "SinhVien",
                column: "MaKhoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "Khoa");
        }
    }
}
