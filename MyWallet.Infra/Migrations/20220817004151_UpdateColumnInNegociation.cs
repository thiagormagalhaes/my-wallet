using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWallet.Infra.Migrations
{
    public partial class UpdateColumnInNegociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Negociation_Company_CompanyId",
                table: "Negociation");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Negociation",
                newName: "TickerId");

            migrationBuilder.RenameIndex(
                name: "IX_Negociation_CompanyId",
                table: "Negociation",
                newName: "IX_Negociation_TickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Negociation_Ticker_TickerId",
                table: "Negociation",
                column: "TickerId",
                principalTable: "Ticker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Negociation_Ticker_TickerId",
                table: "Negociation");

            migrationBuilder.RenameColumn(
                name: "TickerId",
                table: "Negociation",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Negociation_TickerId",
                table: "Negociation",
                newName: "IX_Negociation_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Negociation_Company_CompanyId",
                table: "Negociation",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
