using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiaryApp.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    ProfileImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CommonLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    PageID = table.Column<int>(nullable: false),
                    IdeasAreaID = table.Column<int>(nullable: true),
                    DesiresAreaID = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HabitsTrackers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalName = table.Column<string>(maxLength: 100, nullable: false),
                    SelectedDays = table.Column<string>(nullable: true),
                    GoalsAreaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitsTrackers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PageBase",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    PurchasesAreaID = table.Column<int>(nullable: true),
                    DesiresAreaID = table.Column<int>(nullable: true),
                    IdeasAreaID = table.Column<int>(nullable: true),
                    GoalsAreaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageBase", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PageBase_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventLists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    PageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventLists_PageBase_PageID",
                        column: x => x.PageID,
                        principalTable: "PageBase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageAreas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(maxLength: 100, nullable: false),
                    PageID = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PageAreas_PageBase_PageID",
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
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
                        name: "FK_TodoLists_PageAreas_PurchasesAreaID",
                        column: x => x.PurchasesAreaID,
                        principalTable: "PageAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 200, nullable: true),
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
                name: "IX_CommonLists_DesiresAreaID",
                table: "CommonLists",
                column: "DesiresAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_CommonLists_IdeasAreaID",
                table: "CommonLists",
                column: "IdeasAreaID",
                unique: true,
                filter: "[IdeasAreaID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CommonLists_PageID",
                table: "CommonLists",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_EventLists_PageID",
                table: "EventLists",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerID",
                table: "Events",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_HabitsTrackers_GoalsAreaID",
                table: "HabitsTrackers",
                column: "GoalsAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_OwnerID",
                table: "ListItems",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_PageAreas_PageID",
                table: "PageAreas",
                column: "PageID");

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
                name: "IX_PageBase_UserID",
                table: "PageBase",
                column: "UserID");

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
                name: "FK_ListItems_CommonLists_OwnerID",
                table: "ListItems",
                column: "OwnerID",
                principalTable: "CommonLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventLists_OwnerID",
                table: "Events",
                column: "OwnerID",
                principalTable: "EventLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonLists_PageAreas_DesiresAreaID",
                table: "CommonLists",
                column: "DesiresAreaID",
                principalTable: "PageAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonLists_PageAreas_IdeasAreaID",
                table: "CommonLists",
                column: "IdeasAreaID",
                principalTable: "PageAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommonLists_PageBase_PageID",
                table: "CommonLists",
                column: "PageID",
                principalTable: "PageBase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HabitsTrackers_PageAreas_GoalsAreaID",
                table: "HabitsTrackers",
                column: "GoalsAreaID",
                principalTable: "PageAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_PageAreas_DesiresAreaID",
                table: "PageBase",
                column: "DesiresAreaID",
                principalTable: "PageAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_PageAreas_GoalsAreaID",
                table: "PageBase",
                column: "GoalsAreaID",
                principalTable: "PageAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_PageAreas_IdeasAreaID",
                table: "PageBase",
                column: "IdeasAreaID",
                principalTable: "PageAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_PageAreas_PurchasesAreaID",
                table: "PageBase",
                column: "PurchasesAreaID",
                principalTable: "PageAreas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_PageAreas_DesiresAreaID",
                table: "PageBase");

            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_PageAreas_GoalsAreaID",
                table: "PageBase");

            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_PageAreas_IdeasAreaID",
                table: "PageBase");

            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_PageAreas_PurchasesAreaID",
                table: "PageBase");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "HabitsTrackers");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "EventLists");

            migrationBuilder.DropTable(
                name: "CommonLists");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "PageAreas");

            migrationBuilder.DropTable(
                name: "PageBase");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
