using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class paymentsandbalances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobBids_Users_FreelancerUserId",
                table: "JobBids");

            migrationBuilder.CreateTable(
                name: "SystemBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SystemPaymentId = table.Column<Guid>(nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemBalances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    MilestoneRequestPayerId = table.Column<Guid>(nullable: false),
                    MilestoneSystemRequestPayerId = table.Column<Guid>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBalances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    MilestoneRequestPayerId = table.Column<Guid>(nullable: false),
                    MilestoneSystemRequestPayerId = table.Column<Guid>(nullable: false),
                    TransactionTypeId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPayments_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPayments_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 4,
                column: "StatusDescription",
                value: "Admin approved. Milestone funds paid to freelancer wallet");

            migrationBuilder.UpdateData(
                table: "MilestoneStatus",
                keyColumn: "MilestoneStatusId",
                keyValue: 5,
                column: "StatusDescription",
                value: "Paid after freelancer withdrawal");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPayments_MilestoneRequestPayerId",
                table: "SystemPayments",
                column: "MilestoneRequestPayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPayments_MilestoneSystemRequestPayerId",
                table: "SystemPayments",
                column: "MilestoneSystemRequestPayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPayments_ToUserId",
                table: "SystemPayments",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBalances_UserId",
                table: "UserBalances",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_FromUserId",
                table: "UserPayments",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_ToUserId",
                table: "UserPayments",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobBids_Users_FreelancerUserId",
                table: "JobBids",
                column: "FreelancerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobBids_Users_FreelancerUserId",
                table: "JobBids");

            migrationBuilder.DropTable(
                name: "SystemBalances");

            migrationBuilder.DropTable(
                name: "SystemPayments");

            migrationBuilder.DropTable(
                name: "UserBalances");

            migrationBuilder.DropTable(
                name: "UserPayments");

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

            migrationBuilder.AddForeignKey(
                name: "FK_JobBids_Users_FreelancerUserId",
                table: "JobBids",
                column: "FreelancerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
