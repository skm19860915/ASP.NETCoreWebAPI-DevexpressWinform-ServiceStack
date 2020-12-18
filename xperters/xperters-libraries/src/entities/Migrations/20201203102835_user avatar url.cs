using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class useravatarurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "varchar(1024)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResponseMessage",
                table: "MilestoneSystemRequestPayers",
                type: "varchar(1024)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaymentServiceStatusCheck",
                table: "MilestoneSystemRequestPayers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastPaymentServiceStatusCheck",
                table: "MilestoneSystemRequestPayers");

            migrationBuilder.AlterColumn<string>(
                name: "ResponseMessage",
                table: "MilestoneSystemRequestPayers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1024)",
                oldNullable: true);
        }
    }
}
