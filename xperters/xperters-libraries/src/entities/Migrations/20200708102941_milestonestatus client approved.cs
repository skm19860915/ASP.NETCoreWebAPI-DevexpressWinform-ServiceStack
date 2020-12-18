using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class milestonestatusclientapproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MilestoneStatus",
                columns: new[] { "MilestoneStatusId", "IsActive", "StatusDescription" },
                values: new object[,]
                {
                    { 8, true, "Client Approved" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 8);
        }
    }
}
