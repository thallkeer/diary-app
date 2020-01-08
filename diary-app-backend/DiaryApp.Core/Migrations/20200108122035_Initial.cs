using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiaryApp.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ProfileImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageBase",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    TodoListID = table.Column<int>(nullable: true),
                    EventListID = table.Column<int>(nullable: true),
                    PurchasesAreaID = table.Column<int>(nullable: true),
                    DesiresAreaID = table.Column<int>(nullable: true),
                    IdeasAreaID = table.Column<int>(nullable: true),
                    GoalsAreaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageBase", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PageBase_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DesiresAreas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Header = table.Column<string>(nullable: false),
                    PageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiresAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesiresAreas_PageBase_PageID",
                        column: x => x.PageID,
                        principalTable: "PageBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalsAreas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Header = table.Column<string>(nullable: false),
                    PageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalsAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GoalsAreas_PageBase_PageID",
                        column: x => x.PageID,
                        principalTable: "PageBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdeasAreas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Header = table.Column<string>(nullable: false),
                    PageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeasAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IdeasAreas_PageBase_PageID",
                        column: x => x.PageID,
                        principalTable: "PageBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasesAreas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Header = table.Column<string>(nullable: false),
                    PageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasesAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchasesAreas_PageBase_PageID",
                        column: x => x.PageID,
                        principalTable: "PageBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitsTrackers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    GoalName = table.Column<string>(nullable: false),
                    SelectedDays = table.Column<string>(nullable: true),
                    GoalsAreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitsTrackers", x => x.ID);
                    table.UniqueConstraint("AK_HabitsTrackers_Year_Month_GoalName", x => new { x.Year, x.Month, x.GoalName });
                    table.ForeignKey(
                        name: "FK_HabitsTrackers_GoalsAreas_GoalsAreaID",
                        column: x => x.GoalsAreaID,
                        principalTable: "GoalsAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    PageID = table.Column<int>(nullable: false),
                    DesiresAreaID = table.Column<int>(nullable: true),
                    IdeasAreaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventLists_DesiresAreas_DesiresAreaID",
                        column: x => x.DesiresAreaID,
                        principalTable: "DesiresAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventLists_IdeasAreas_IdeasAreaID",
                        column: x => x.IdeasAreaID,
                        principalTable: "IdeasAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventLists_PageBase_PageID",
                        column: x => x.PageID,
                        principalTable: "PageBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    PageID = table.Column<int>(nullable: false),
                    PurchasesAreaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TodoLists_PageBase_PageID",
                        column: x => x.PageID,
                        principalTable: "PageBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TodoLists_PurchasesAreas_PurchasesAreaID",
                        column: x => x.PurchasesAreaID,
                        principalTable: "PurchasesAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(nullable: true),
                    OwnerID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    FullDay = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Events_EventLists_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "EventLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(nullable: true),
                    OwnerID = table.Column<int>(nullable: false),
                    Done = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Todos_TodoLists_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "TodoLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DesiresAreas_PageID",
                table: "DesiresAreas",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_EventLists_DesiresAreaID",
                table: "EventLists",
                column: "DesiresAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_EventLists_IdeasAreaID",
                table: "EventLists",
                column: "IdeasAreaID",
                unique: true,
                filter: "[IdeasAreaID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventLists_PageID",
                table: "EventLists",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerID",
                table: "Events",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_GoalsAreas_PageID",
                table: "GoalsAreas",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_HabitsTrackers_GoalsAreaID",
                table: "HabitsTrackers",
                column: "GoalsAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_IdeasAreas_PageID",
                table: "IdeasAreas",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_EventListID",
                table: "PageBase",
                column: "EventListID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_TodoListID",
                table: "PageBase",
                column: "TodoListID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_DesiresAreaID",
                table: "PageBase",
                column: "DesiresAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_GoalsAreaID",
                table: "PageBase",
                column: "GoalsAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_IdeasAreaID",
                table: "PageBase",
                column: "IdeasAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_PurchasesAreaID",
                table: "PageBase",
                column: "PurchasesAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_UserId",
                table: "PageBase",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesAreas_PageID",
                table: "PurchasesAreas",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_TodoLists_PageID",
                table: "TodoLists",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_TodoLists_PurchasesAreaID",
                table: "TodoLists",
                column: "PurchasesAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_OwnerID",
                table: "Todos",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_DesiresAreas_DesiresAreaID",
                table: "PageBase",
                column: "DesiresAreaID",
                principalTable: "DesiresAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_IdeasAreas_IdeasAreaID",
                table: "PageBase",
                column: "IdeasAreaID",
                principalTable: "IdeasAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_EventLists_EventListID",
                table: "PageBase",
                column: "EventListID",
                principalTable: "EventLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_GoalsAreas_GoalsAreaID",
                table: "PageBase",
                column: "GoalsAreaID",
                principalTable: "GoalsAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_TodoLists_TodoListID",
                table: "PageBase",
                column: "TodoListID",
                principalTable: "TodoLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_PurchasesAreas_PurchasesAreaID",
                table: "PageBase",
                column: "PurchasesAreaID",
                principalTable: "PurchasesAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_AspNetUsers_UserId",
                table: "PageBase");

            migrationBuilder.DropForeignKey(
                name: "FK_DesiresAreas_PageBase_PageID",
                table: "DesiresAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLists_PageBase_PageID",
                table: "EventLists");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalsAreas_PageBase_PageID",
                table: "GoalsAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeasAreas_PageBase_PageID",
                table: "IdeasAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesAreas_PageBase_PageID",
                table: "PurchasesAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoLists_PageBase_PageID",
                table: "TodoLists");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "HabitsTrackers");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PageBase");

            migrationBuilder.DropTable(
                name: "EventLists");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "GoalsAreas");

            migrationBuilder.DropTable(
                name: "DesiresAreas");

            migrationBuilder.DropTable(
                name: "IdeasAreas");

            migrationBuilder.DropTable(
                name: "PurchasesAreas");
        }
    }
}
