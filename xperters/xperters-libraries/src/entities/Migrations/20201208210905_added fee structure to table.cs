using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class addedfeestructuretotable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("1caff75a-9413-48a6-bf8f-8872cdd0d7f7"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("aae78db6-0b5b-48d2-9ef0-b96d70a9da70"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("b370a1f6-79e4-429e-aaf8-cdbe397c263b"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("d77307d8-cb7f-4c66-a196-d8e087eebdd1"));

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "Id",
                keyValue: new Guid("fde408e1-608d-433e-9a53-0e3ecfe56707"));

            migrationBuilder.RenameColumn(
                name: "Fee",
                table: "MilestoneRequestPayers",
                newName: "FeeTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "FeeFlat",
                table: "SystemPayments",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePercent",
                table: "SystemPayments",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeFlat",
                table: "MilestoneRequestPayers",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePercent",
                table: "MilestoneRequestPayers",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeFlatRate",
                table: "Fees",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePercentage",
                table: "Fees",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "FeeFlat",
                table: "SystemPayments");

            migrationBuilder.DropColumn(
                name: "FeePercent",
                table: "SystemPayments");

            migrationBuilder.DropColumn(
                name: "FeeFlat",
                table: "MilestoneRequestPayers");

            migrationBuilder.DropColumn(
                name: "FeePercent",
                table: "MilestoneRequestPayers");

            migrationBuilder.DropColumn(
                name: "FeeFlatRate",
                table: "Fees");

            migrationBuilder.DropColumn(
                name: "FeePercentage",
                table: "Fees");

            migrationBuilder.RenameColumn(
                name: "FeeTotal",
                table: "MilestoneRequestPayers",
                newName: "Fee");

            migrationBuilder.InsertData(
                table: "Fees",
                columns: new[] { "Id", "BandEnd", "BandStart", "CreatedDate", "Description", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("b370a1f6-79e4-429e-aaf8-cdbe397c263b"), 500m, 0m, new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(5040), "0-500", new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(5420) },
                    { new Guid("d77307d8-cb7f-4c66-a196-d8e087eebdd1"), 1000m, 500m, new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7180), "500-1000", new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7210) },
                    { new Guid("1caff75a-9413-48a6-bf8f-8872cdd0d7f7"), 10000m, 1000m, new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7250), "1000-10000", new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7250) },
                    { new Guid("fde408e1-608d-433e-9a53-0e3ecfe56707"), 100000m, 10000m, new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7250), "10000-100000", new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7250) },
                    { new Guid("aae78db6-0b5b-48d2-9ef0-b96d70a9da70"), 0m, 100000m, new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7260), ">100000", new DateTime(2020, 12, 8, 10, 1, 28, 198, DateTimeKind.Utc).AddTicks(7260) }
                });
        }
    }
}
