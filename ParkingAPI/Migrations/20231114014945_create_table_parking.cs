using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingAPI.Migrations
{
    public partial class create_table_parking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Vehicle",
                newName: "modify_date");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Company",
                newName: "modify_date");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Address",
                newName: "modify_date");

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    company_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    vehicle_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreateAT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    modify_date = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parking_Company_company_id",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Parking_Vehicle_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_company_id",
                table: "Parking",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_vehicle_id",
                table: "Parking",
                column: "vehicle_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.RenameColumn(
                name: "modify_date",
                table: "Vehicle",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "modify_date",
                table: "Company",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "modify_date",
                table: "Address",
                newName: "UpdateAt");
        }
    }
}
