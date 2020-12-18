using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class staticguidsforfeestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fees",
                table: "Fees");

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("5638432d-e6c9-422b-b52b-16c1aa24420d"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("952725c1-aa5b-4ac4-a590-07cf8c58e913"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("b50cf5d0-cb99-44ad-b118-0437a78fd330"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("d81ca79e-08e8-4bc3-ba9c-af5a047f1427"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("ffeb9fc9-15b1-4f94-ae8c-b14de2a27edc"));

            migrationBuilder.RenameTable(
                name: "Fees",
                newName: "FeeStructures");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "SystemPayments",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeeStructures",
                table: "FeeStructures",
                column: "Id");

            migrationBuilder.InsertData(
                table: "FeeStructures",
                columns: new[] { "Id", "BandEnd", "BandStart", "CreatedDate", "Description", "FeeFlatRate", "FeePercentage", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-0000-0000-0000-000000000001"), 500m, 0m, new DateTime(2020, 12, 10, 19, 40, 57, 26, DateTimeKind.Utc).AddTicks(3022), "0-500", 20m, 5m, new DateTime(2020, 12, 10, 19, 40, 57, 26, DateTimeKind.Utc).AddTicks(6319) },
                    { new Guid("11111111-0000-0000-0000-000000000002"), 1000m, 500m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(815), "500-1000", 15m, 5m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(873) },
                    { new Guid("11111111-0000-0000-0000-000000000003"), 10000m, 1000m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(1054), "1000-10000", 10m, 4m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(1057) },
                    { new Guid("11111111-0000-0000-0000-000000000004"), 100000m, 10000m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(1070), "10000-100000", 10m, 3.5m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(1071) },
                    { new Guid("11111111-0000-0000-0000-000000000005"), 0m, 100000m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(1628), ">100000", 10m, 3m, new DateTime(2020, 12, 10, 19, 40, 57, 29, DateTimeKind.Utc).AddTicks(1640) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FeeStructures",
                table: "FeeStructures");

            migrationBuilder.DeleteData(
                table: "FeeStructures",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "FeeStructures",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "FeeStructures",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "FeeStructures",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "FeeStructures",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000005"));

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "SystemPayments");

            migrationBuilder.RenameTable(
                name: "FeeStructures",
                newName: "Fees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fees",
                table: "Fees",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Fees",
                columns: new[] { "Id", "BandEnd", "BandStart", "CreatedDate", "Description", "FeeFlatRate", "FeePercentage", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("5638432d-e6c9-422b-b52b-16c1aa24420d"), 500m, 0m, new DateTime(2020, 12, 8, 21, 9, 3, 111, DateTimeKind.Utc).AddTicks(7690), "0-500", 20m, 5m, new DateTime(2020, 12, 8, 21, 9, 3, 111, DateTimeKind.Utc).AddTicks(8090) },
                    { new Guid("d81ca79e-08e8-4bc3-ba9c-af5a047f1427"), 1000m, 500m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(650), "500-1000", 15m, 5m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(690) },
                    { new Guid("ffeb9fc9-15b1-4f94-ae8c-b14de2a27edc"), 10000m, 1000m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(740), "1000-10000", 10m, 4m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(740) },
                    { new Guid("b50cf5d0-cb99-44ad-b118-0437a78fd330"), 100000m, 10000m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(750), "10000-100000", 10m, 3.5m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(750) },
                    { new Guid("952725c1-aa5b-4ac4-a590-07cf8c58e913"), 0m, 100000m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(780), ">100000", 10m, 3m, new DateTime(2020, 12, 8, 21, 9, 3, 112, DateTimeKind.Utc).AddTicks(780) }
                });
        }
    }
}
