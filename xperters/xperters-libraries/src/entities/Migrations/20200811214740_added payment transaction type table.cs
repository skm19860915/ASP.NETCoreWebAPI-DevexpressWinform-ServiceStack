using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class addedpaymenttransactiontypetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "UserPayments");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "SystemPayments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentTransactionTypeId",
                table: "UserPayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTransactionTypeId",
                table: "SystemPayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PaymentTransactionTypes",
                columns: table => new
                {
                    PaymentTransactionTypeId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactionTypes", x => x.PaymentTransactionTypeId);
                });

            migrationBuilder.InsertData(
                table: "PaymentTransactionTypes",
                columns: new[] { "PaymentTransactionTypeId", "Type" },
                values: new object[] { 1, "Credit" });

            migrationBuilder.InsertData(
                table: "PaymentTransactionTypes",
                columns: new[] { "PaymentTransactionTypeId", "Type" },
                values: new object[] { 2, "Debit" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_PaymentTransactionTypeId",
                table: "UserPayments",
                column: "PaymentTransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPayments_PaymentTransactionTypeId",
                table: "SystemPayments",
                column: "PaymentTransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemPayments_PaymentTransactionTypes_PaymentTransactionTypeId",
                table: "SystemPayments",
                column: "PaymentTransactionTypeId",
                principalTable: "PaymentTransactionTypes",
                principalColumn: "PaymentTransactionTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayments_PaymentTransactionTypes_PaymentTransactionTypeId",
                table: "UserPayments",
                column: "PaymentTransactionTypeId",
                principalTable: "PaymentTransactionTypes",
                principalColumn: "PaymentTransactionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemPayments_PaymentTransactionTypes_PaymentTransactionTypeId",
                table: "SystemPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPayments_PaymentTransactionTypes_PaymentTransactionTypeId",
                table: "UserPayments");

            migrationBuilder.DropTable(
                name: "PaymentTransactionTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserPayments_PaymentTransactionTypeId",
                table: "UserPayments");

            migrationBuilder.DropIndex(
                name: "IX_SystemPayments_PaymentTransactionTypeId",
                table: "SystemPayments");

            migrationBuilder.DropColumn(
                name: "PaymentTransactionTypeId",
                table: "UserPayments");

            migrationBuilder.DropColumn(
                name: "PaymentTransactionTypeId",
                table: "SystemPayments");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "UserPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "SystemPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
