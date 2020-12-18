using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class Fundingstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MilestoneStatus",
                columns: new[] { "MilestoneStatusId", "IsActive", "StatusDescription" },
                values: new object[,]
                {
                    { 7, true, "Funded" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 7);
        }
    }
}
