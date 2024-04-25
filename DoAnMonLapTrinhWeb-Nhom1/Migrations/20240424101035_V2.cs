using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnMonLapTrinhWeb_Nhom1.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__HoaDon__75A278F5",
                table: "HoaDon");

            migrationBuilder.DropPrimaryKey(
                name: "PK__YeuCauDa__83A900AC0247BA4F",
                table: "YeuCauDatXe");

            migrationBuilder.DropIndex(
                name: "IX_HoaDon_Email_BienSoXe",
                table: "HoaDon");

            migrationBuilder.AddColumn<int>(
                name: "MaYeuCau",
                table: "YeuCauDatXe",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MaYeuCau",
                table: "HoaDon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK__YeuCauDatXe__MaYeuCau",
                table: "YeuCauDatXe",
                column: "MaYeuCau");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauDatXe_Email",
                table: "YeuCauDatXe",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaYeuCau",
                table: "HoaDon",
                column: "MaYeuCau");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK__YeuCauDatXe__MaYeuCau",
                table: "YeuCauDatXe");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauDatXe_Email",
                table: "YeuCauDatXe");

            migrationBuilder.DropIndex(
                name: "IX_HoaDon_MaYeuCau",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "MaYeuCau",
                table: "YeuCauDatXe");

            migrationBuilder.DropColumn(
                name: "MaYeuCau",
                table: "HoaDon");

            migrationBuilder.AddPrimaryKey(
                name: "PK__YeuCauDa__83A900AC0247BA4F",
                table: "YeuCauDatXe",
                columns: new[] { "Email", "BienSoXe" });

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Email_BienSoXe",
                table: "HoaDon",
                columns: new[] { "Email", "BienSoXe" });

            migrationBuilder.AddForeignKey(
                name: "FK__HoaDon__75A278F5",
                table: "HoaDon",
                columns: new[] { "Email", "BienSoXe" },
                principalTable: "YeuCauDatXe",
                principalColumns: new[] { "Email", "BienSoXe" });
        }
    }
}
