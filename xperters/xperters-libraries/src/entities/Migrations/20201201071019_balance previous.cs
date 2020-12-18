using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class balanceprevious : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BalancePrevious",
                table: "UserBalances",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransactionAmount",
                table: "UserBalances",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BalancePrevious",
                table: "SystemBalances",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TransactionAmount",
                table: "SystemBalances",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalancePrevious",
                table: "UserBalances");

            migrationBuilder.DropColumn(
                name: "TransactionAmount",
                table: "UserBalances");

            migrationBuilder.DropColumn(
                name: "BalancePrevious",
                table: "SystemBalances");

            migrationBuilder.DropColumn(
                name: "TransactionAmount",
                table: "SystemBalances");
        }
    }
}
