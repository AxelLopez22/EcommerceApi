using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerceApi.Migrations
{
    public partial class UpdateSistemaUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Compras",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_AspNetUsers_UsuarioId",
                table: "Compras",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_AspNetUsers_UsuarioId",
                table: "Compras");

            migrationBuilder.DropIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Compras");
        }
    }
}
