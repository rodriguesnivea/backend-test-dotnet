using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingAPI.Migrations
{
    public partial class modify_name_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAT",
                table: "Vehicle",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "CreateAT",
                table: "Parking",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "CreateAT",
                table: "Company",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "CreateAT",
                table: "Address",
                newName: "start_date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Vehicle",
                newName: "CreateAT");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Parking",
                newName: "CreateAT");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Company",
                newName: "CreateAT");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Address",
                newName: "CreateAT");
        }
    }
}
