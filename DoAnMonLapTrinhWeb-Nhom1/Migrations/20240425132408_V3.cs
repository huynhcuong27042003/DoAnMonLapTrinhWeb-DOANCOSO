using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnMonLapTrinhWeb_Nhom1.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__HoaDon__75A278F9",
                table: "HoaDon");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnhKhuyenMai",
                table: "KhuyenMai",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK__HoaDon__75A278F5",
                table: "HoaDon",
                column: "MaYeuCau",
                principalTable: "YeuCauDatXe",
                principalColumn: "MaYeuCau");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__HoaDon__75A278F5",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "HinhAnhKhuyenMai",
                table: "KhuyenMai");

            migrationBuilder.AddForeignKey(
                name: "FK__HoaDon__75A278F9",
                table: "HoaDon",
                column: "MaYeuCau",
                principalTable: "YeuCauDatXe",
                principalColumn: "MaYeuCau");
        }
    }
}
