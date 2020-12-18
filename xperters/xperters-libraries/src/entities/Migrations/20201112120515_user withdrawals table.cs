using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class userwithdrawalstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "MilestoneRequestPayers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "MilestoneRequestPayers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "UserWithdrawals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BalanceOld = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    BalanceNew = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    PayerStatusId = table.Column<int>(nullable: false),
                    ResponseMessage = table.Column<string>(type: "varchar(1024)", nullable: true),
                    LastPaymentServiceStatusCheck = table.Column<DateTime>(nullable: true),
                    PaymentServiceCheckCount = table.Column<int>(nullable: true),
                    CompletedDate = table.Column<DateTime>(nullable: true),
                    PaymentTransactionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWithdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWithdrawals_RequestPayerStatus_PayerStatusId",
                        column: x => x.PayerStatusId,
                        principalTable: "RequestPayerStatus",
                        principalColumn: "PayerStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWithdrawals_PaymentTransactionTypes_PaymentTransactionTypeId",
                        column: x => x.PaymentTransactionTypeId,
                        principalTable: "PaymentTransactionTypes",
                        principalColumn: "PaymentTransactionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWithdrawals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "CurrencyCode" },
                values: new object[] { 2, "EUR" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWithdrawals_PayerStatusId",
                table: "UserWithdrawals",
                column: "PayerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWithdrawals_PaymentTransactionTypeId",
                table: "UserWithdrawals",
                column: "PaymentTransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWithdrawals_UserId",
                table: "UserWithdrawals",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWithdrawals");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "CurrencyId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "MilestoneRequestPayers");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "MilestoneRequestPayers");
        }
    }
}
