using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace xperters.entities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "varchar(150)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    JobStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "varchar(50)", nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.JobStatusId);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneStatus",
                columns: table => new
                {
                    MilestoneStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDescription = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneStatus", x => x.MilestoneStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RequestPayerStatus",
                columns: table => new
                {
                    PayerStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayerStatus = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestPayerStatus", x => x.PayerStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    MobilePhone = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 255, nullable: true),
                    LastName = table.Column<string>(maxLength: 255, nullable: true),
                    UserRole = table.Column<byte>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    AccountHolderName = table.Column<string>(type: "varchar(100)", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "varchar(100)", nullable: false),
                    BankName = table.Column<string>(type: "varchar(100)", nullable: false),
                    BranchName = table.Column<string>(type: "varchar(100)", nullable: true),
                    IfscCode = table.Column<string>(type: "varchar(50)", nullable: true),
                    SwiftNumber = table.Column<string>(type: "varchar(100)", nullable: false),
                    BankAddress = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsNotSupported = table.Column<bool>(nullable: false),
                    Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    CardScheme = table.Column<int>(nullable: false),
                    CardType = table.Column<int>(nullable: false),
                    ExpMonth = table.Column<int>(nullable: false),
                    ExpYear = table.Column<int>(nullable: false),
                    Number = table.Column<string>(type: "varchar(30)", nullable: false),
                    NumberSuffix = table.Column<string>(type: "char(4)", nullable: false),
                    AddressCity = table.Column<string>(type: "varchar(100)", nullable: true),
                    AddressCountry = table.Column<string>(type: "varchar(100)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "varchar(100)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "varchar(100)", nullable: true),
                    AddressState = table.Column<string>(type: "varchar(100)", nullable: true),
                    AddressZip = table.Column<string>(type: "varchar(20)", nullable: true),
                    Currency = table.Column<string>(type: "varchar(100)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    IssuingCardId = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailAudits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    SenderEmailAddress = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<Guid>(nullable: false),
                    ReceiverEmailAddress = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAudits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAudits_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    JobTitle = table.Column<string>(type: "varchar(255)", nullable: false),
                    SelectedJobCategory = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "varchar(4000)", nullable: false),
                    JobTypeId = table.Column<int>(nullable: false),
                    JobVisibility = table.Column<int>(nullable: false),
                    FreelancersStrength = table.Column<int>(nullable: false),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    ExperienceLevel = table.Column<int>(nullable: false),
                    JobDuration = table.Column<int>(nullable: false),
                    FreelancerTypeId = table.Column<int>(nullable: false),
                    ClientHistory = table.Column<int>(nullable: false),
                    JobPrice = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsDraft = table.Column<bool>(nullable: false),
                    JobStatusId = table.Column<int>(nullable: false),
                    EstimatedBudgetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_JobStatus_JobStatusId",
                        column: x => x.JobStatusId,
                        principalTable: "JobStatus",
                        principalColumn: "JobStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Uri = table.Column<string>(type: "varchar(2083)", nullable: true),
                    MimeType = table.Column<string>(type: "varchar(255)", nullable: false),
                    LocalPath = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    EmailAuditId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAttachments_EmailAudits_EmailAuditId",
                        column: x => x.EmailAuditId,
                        principalTable: "EmailAudits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractChatSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JobId = table.Column<Guid>(nullable: false),
                    FreelancerId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractChatSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractChatSessions_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractChatSessions_Users_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractChatSessions_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Uri = table.Column<string>(type: "varchar(2083)", nullable: true),
                    MimeType = table.Column<string>(type: "varchar(255)", nullable: false),
                    LocalPath = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    JobId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobAttachments_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobBidChatSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JobId = table.Column<Guid>(nullable: false),
                    FreelancerId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBidChatSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBidChatSessions_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobBidChatSessions_Users_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobBidChatSessions_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobBids",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Message = table.Column<string>(type: "varchar(4000)", nullable: false),
                    JobId = table.Column<Guid>(nullable: false),
                    FreelancerUserId = table.Column<Guid>(nullable: false),
                    BidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BidStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBids_Users_FreelancerUserId",
                        column: x => x.FreelancerUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobBids_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JobId = table.Column<Guid>(nullable: false),
                    FreelancerId = table.Column<Guid>(nullable: false),
                    ContractStartDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    ContractStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobContracts_Users_FreelancerId",
                        column: x => x.FreelancerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobContracts_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    ContractChatSessionId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(type: "varchar(4000)", nullable: true),
                    MsgType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractChatMessages_ContractChatSessions_ContractChatSessionId",
                        column: x => x.ContractChatSessionId,
                        principalTable: "ContractChatSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractChatSessionUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ContractChatSessionId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractChatSessionUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractChatSessionUsers_ContractChatSessions_ContractChatSessionId",
                        column: x => x.ContractChatSessionId,
                        principalTable: "ContractChatSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractChatSessionUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobBidChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SenderId = table.Column<Guid>(nullable: false),
                    JobBidChatSessionId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(type: "varchar(4000)", nullable: true),
                    MessageType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBidChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBidChatMessages_JobBidChatSessions_JobBidChatSessionId",
                        column: x => x.JobBidChatSessionId,
                        principalTable: "JobBidChatSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobBidChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobBidChatSessionUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JobBidChatSessionId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBidChatSessionUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBidChatSessionUsers_JobBidChatSessions_JobBidChatSessionId",
                        column: x => x.JobBidChatSessionId,
                        principalTable: "JobBidChatSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobBidChatSessionUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobBidAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Uri = table.Column<string>(type: "varchar(2083)", nullable: true),
                    MimeType = table.Column<string>(type: "varchar(255)", nullable: false),
                    LocalPath = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    JobBidId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobBidAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobBidAttachments_JobBids_JobBidId",
                        column: x => x.JobBidId,
                        principalTable: "JobBids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MilestoneDescription = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    MilestoneStatus = table.Column<int>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestones_JobContracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "JobContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Milestones_Users_CreatedId",
                        column: x => x.CreatedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractMilestoneFunds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MilestoneId = table.Column<Guid>(nullable: false),
                    FundStatus = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractMilestoneFunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractMilestoneFunds_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractMilestoneFunds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Uri = table.Column<string>(type: "varchar(2083)", nullable: true),
                    MimeType = table.Column<string>(type: "varchar(255)", nullable: false),
                    LocalPath = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", nullable: false),
                    FileSize = table.Column<long>(nullable: false),
                    MilestoneId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilestoneAttachments_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MilestoneId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", nullable: true),
                    CreatedId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilestoneMessage_Users_CreatedId",
                        column: x => x.CreatedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilestoneMessage_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneRequestPayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MilestoneId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal", nullable: false),
                    PayerStatusId = table.Column<int>(nullable: false),
                    ResponseMessage = table.Column<string>(type: "varchar(1024)", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PaymentServiceCheckCount = table.Column<int>(nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneRequestPayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilestoneRequestPayers_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilestoneRequestPayers_RequestPayerStatus_PayerStatusId",
                        column: x => x.PayerStatusId,
                        principalTable: "RequestPayerStatus",
                        principalColumn: "PayerStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MilestoneSystemRequestPayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MilestoneId = table.Column<Guid>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal", nullable: false),
                    PayerStatusId = table.Column<int>(nullable: false),
                    ResponseMessage = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PaymentServiceCheckCount = table.Column<int>(nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilestoneSystemRequestPayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MilestoneSystemRequestPayers_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "Milestones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilestoneSystemRequestPayers_RequestPayerStatus_PayerStatusId",
                        column: x => x.PayerStatusId,
                        principalTable: "RequestPayerStatus",
                        principalColumn: "PayerStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "IsActive" },
                values: new object[,]
                {
                    { 1, "Web, Mobile & Software Dev", true },
                    { 21, "Other - Sales & Marketing", true },
                    { 20, "Telemarketing & Telesales", true },
                    { 19, "SEO - Search Engine Optimization", true },
                    { 18, "SEM - Search Engine Marketing", true },
                    { 17, "Public Relations", true },
                    { 16, "Marketing Strategy", true },
                    { 15, "Market & Customer Research", true },
                    { 14, "Lead Generation", true },
                    { 13, "Email & Marketing Automation", true },
                    { 22, "Accounting & Consulting", true },
                    { 12, "Display Advertising", true },
                    { 10, "Customer Service", true },
                    { 9, "Admin Support", true },
                    { 8, "Legal", true },
                    { 7, "Translation", true },
                    { 6, "Writing", true },
                    { 5, "Design & Creative", true },
                    { 4, "Engineering & Architecture", true },
                    { 3, "Data Science & Analytics", true },
                    { 2, "IT & Networking", true },
                    { 11, "Sales & Marketing", true }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryName", "IsActive" },
                values: new object[,]
                {
                    { 91, "India", true },
                    { 44, "United Kingdom", true },
                    { 256, "Uganda", true },
                    { 1, "United States", true }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "CurrencyCode" },
                values: new object[] { 1, "USD" });

            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "JobStatusId", "IsActive", "Status" },
                values: new object[,]
                {
                    { 1, true, "Job Posted" },
                    { 2, true, "ContractSigned" },
                    { 3, true, "Job In-Progress" },
                    { 4, true, "Job Completed" },
                    { 5, true, "Job Canceled" }
                });

            migrationBuilder.InsertData(
                table: "MilestoneStatus",
                columns: new[] { "MilestoneStatusId", "IsActive", "StatusDescription" },
                values: new object[,]
                {
                   
                    { 5, true, "Paid" },
                    { 6, true, "Cancelled" },
                    { 4, true, "Admin Approved" },
                    { 3, true, "Freelancer Closed. Waiting For ClientApproval" },
                    { 2, true, "Active" },
                    { 1, true, "Add Funds" }
                });

            migrationBuilder.InsertData(
                table: "RequestPayerStatus",
                columns: new[] { "PayerStatusId", "IsActive", "PayerStatus" },
                values: new object[,]
                {
                    { 1, true, "Successful" },
                    { 2, true, "Pending" },
                    { 3, true, "Cancelled" },
                    { 4, true, "Failed" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "IsActive", "SkillName" },
                values: new object[,]
                {
                    { 6, true, "JQuery" },
                    { 5, true, "Javascript" },
                    { 4, true, "AngularJs" },
                    { 7, true, "MongoDB" },
                    { 2, true, "C#" },
                    { 1, true, "C++" },
                    { 3, true, "OOPS" },
                    { 8, true, "SQL Server" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetails_UserId",
                table: "AccountDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChatMessages_ContractChatSessionId",
                table: "ContractChatMessages",
                column: "ContractChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChatMessages_SenderId",
                table: "ContractChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChatSessions_ClientId",
                table: "ContractChatSessions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChatSessions_FreelancerId",
                table: "ContractChatSessions",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChatSessions_JobId",
                table: "ContractChatSessions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChatSessionUsers_ContractChatSessionId",
                table: "ContractChatSessionUsers",
                column: "ContractChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChatSessionUsers_UserId",
                table: "ContractChatSessionUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMilestoneFunds_MilestoneId",
                table: "ContractMilestoneFunds",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMilestoneFunds_UserId",
                table: "ContractMilestoneFunds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAttachments_EmailAuditId",
                table: "EmailAttachments",
                column: "EmailAuditId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAudits_ReceiverId",
                table: "EmailAudits",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAttachments_JobId",
                table: "JobAttachments",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidAttachments_JobBidId",
                table: "JobBidAttachments",
                column: "JobBidId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidChatMessages_JobBidChatSessionId",
                table: "JobBidChatMessages",
                column: "JobBidChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidChatMessages_SenderId",
                table: "JobBidChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidChatSessions_ClientId",
                table: "JobBidChatSessions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidChatSessions_FreelancerId",
                table: "JobBidChatSessions",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidChatSessions_JobId",
                table: "JobBidChatSessions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidChatSessionUsers_JobBidChatSessionId",
                table: "JobBidChatSessionUsers",
                column: "JobBidChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBidChatSessionUsers_UserId",
                table: "JobBidChatSessionUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBids_FreelancerUserId",
                table: "JobBids",
                column: "FreelancerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobBids_JobId",
                table: "JobBids",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContracts_FreelancerId",
                table: "JobContracts",
                column: "FreelancerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobContracts_JobId",
                table: "JobContracts",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobStatusId",
                table: "Jobs",
                column: "JobStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UserId",
                table: "Jobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneAttachments_MilestoneId",
                table: "MilestoneAttachments",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneMessage_CreatedId",
                table: "MilestoneMessage",
                column: "CreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneMessage_MilestoneId",
                table: "MilestoneMessage",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneRequestPayers_MilestoneId",
                table: "MilestoneRequestPayers",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneRequestPayers_PayerStatusId",
                table: "MilestoneRequestPayers",
                column: "PayerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_ContractId",
                table: "Milestones",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_CreatedId",
                table: "Milestones",
                column: "CreatedId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneSystemRequestPayers_MilestoneId",
                table: "MilestoneSystemRequestPayers",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MilestoneSystemRequestPayers_PayerStatusId",
                table: "MilestoneSystemRequestPayers",
                column: "PayerStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryId",
                table: "Users",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDetails");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ContractChatMessages");

            migrationBuilder.DropTable(
                name: "ContractChatSessionUsers");

            migrationBuilder.DropTable(
                name: "ContractMilestoneFunds");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "EmailAttachments");

            migrationBuilder.DropTable(
                name: "JobAttachments");

            migrationBuilder.DropTable(
                name: "JobBidAttachments");

            migrationBuilder.DropTable(
                name: "JobBidChatMessages");

            migrationBuilder.DropTable(
                name: "JobBidChatSessionUsers");

            migrationBuilder.DropTable(
                name: "MilestoneAttachments");

            migrationBuilder.DropTable(
                name: "MilestoneMessage");

            migrationBuilder.DropTable(
                name: "MilestoneRequestPayers");

            migrationBuilder.DropTable(
                name: "MilestoneStatus");

            migrationBuilder.DropTable(
                name: "MilestoneSystemRequestPayers");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "ContractChatSessions");

            migrationBuilder.DropTable(
                name: "EmailAudits");

            migrationBuilder.DropTable(
                name: "JobBids");

            migrationBuilder.DropTable(
                name: "JobBidChatSessions");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "RequestPayerStatus");

            migrationBuilder.DropTable(
                name: "JobContracts");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
