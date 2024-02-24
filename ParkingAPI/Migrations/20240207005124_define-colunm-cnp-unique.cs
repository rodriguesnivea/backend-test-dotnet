using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingAPI.Migrations
{
    public partial class definecolunmcnpunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Company_cnpj",
                table: "Company",
                column: "cnpj",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Company_cnpj",
                table: "Company");
        }
    }
}
