using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWallet.Infra.Migrations
{
    public partial class AddPatrimony : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patrimony",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TickerId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrimony", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patrimony_Ticker_TickerId",
                        column: x => x.TickerId,
                        principalTable: "Ticker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patrimony_TickerId",
                table: "Patrimony",
                column: "TickerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patrimony");
        }
    }
}
