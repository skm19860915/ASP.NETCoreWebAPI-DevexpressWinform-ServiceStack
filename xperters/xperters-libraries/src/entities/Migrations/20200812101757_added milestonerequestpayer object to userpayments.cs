using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class addedmilestonerequestpayerobjecttouserpayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_MilestoneRequestPayerId",
                table: "UserPayments",
                column: "MilestoneRequestPayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayments_MilestoneRequestPayers_MilestoneRequestPayerId",
                table: "UserPayments",
                column: "MilestoneRequestPayerId",
                principalTable: "MilestoneRequestPayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPayments_MilestoneRequestPayers_MilestoneRequestPayerId",
                table: "UserPayments");

            migrationBuilder.DropIndex(
                name: "IX_UserPayments_MilestoneRequestPayerId",
                table: "UserPayments");
        }
    }
}
