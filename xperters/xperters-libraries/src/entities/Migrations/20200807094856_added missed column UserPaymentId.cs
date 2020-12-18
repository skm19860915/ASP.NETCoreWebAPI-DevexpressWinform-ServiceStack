using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class addedmissedcolumnUserPaymentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserPaymentId",
                table: "UserBalances",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserBalances_UserPaymentId",
                table: "UserBalances",
                column: "UserPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBalances_UserPayments_UserPaymentId",
                table: "UserBalances",
                column: "UserPaymentId",
                principalTable: "UserPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBalances_UserPayments_UserPaymentId",
                table: "UserBalances");

            migrationBuilder.DropIndex(
                name: "IX_UserBalances_UserPaymentId",
                table: "UserBalances");

            migrationBuilder.DropColumn(
                name: "UserPaymentId",
                table: "UserBalances");
        }
    }
}
