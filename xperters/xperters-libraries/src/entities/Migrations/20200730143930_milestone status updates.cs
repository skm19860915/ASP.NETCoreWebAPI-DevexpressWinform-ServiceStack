using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class milestonestatusupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 3,
                column: "StatusDescription",
                value: "Milestone completed. Waiting For client approval");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 4,
                column: "StatusDescription",
                value: "Admin approved. Milestone funds paid to freelancer account");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 5,
                column: "StatusDescription",
                value: "Paid after client withdrawal");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 6,
                column: "StatusDescription",
                value: "Milestone cancelled by client. Pending refund");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 7,
                column: "StatusDescription",
                value: "Payment rejected");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 8,
                column: "StatusDescription",
                value: "Client approved. Awaiting admin review");


            migrationBuilder.InsertData(
                table: "MilestoneStatus",
                columns: new[] { "MilestoneStatusId", "IsActive", "StatusDescription" },
                values: new object[,]
                {
                    { 9, true, "Freelancer withdrawal" },
                    { 10, true, "Milestone cancelled by freelancer. Pending refund" },
                    { 11, true, "Client refunded" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 3,
                column: "StatusDescription",
                value: "Freelancer Closed. Waiting For ClientApproval");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 4,
                column: "StatusDescription",
                value: "Admin Approved");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 5,
                column: "StatusDescription",
                value: "Paid");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 6,
                column: "StatusDescription",
                value: "Cancelled");
        }
    }
}
