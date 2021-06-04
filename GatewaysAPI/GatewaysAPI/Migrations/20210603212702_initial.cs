using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GatewaysAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gateway",
                columns: table => new
                {
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HRName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPv4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateway", x => x.SerialNumber);
                });

            migrationBuilder.CreateTable(
                name: "Peripherals",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    GatewaySerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peripherals", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Peripherals_Gateway_GatewaySerialNumber",
                        column: x => x.GatewaySerialNumber,
                        principalTable: "Gateway",
                        principalColumn: "SerialNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peripherals_GatewaySerialNumber",
                table: "Peripherals",
                column: "GatewaySerialNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peripherals");

            migrationBuilder.DropTable(
                name: "Gateway");
        }
    }
}
