using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerceApi.Migrations
{
    public partial class UpdateSistemaUserVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Venta",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_UsuarioId",
                table: "Venta",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId",
                table: "Venta",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venta_AspNetUsers_UsuarioId",
                table: "Venta");

            migrationBuilder.DropIndex(
                name: "IX_Venta_UsuarioId",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Venta");
        }
    }
}
