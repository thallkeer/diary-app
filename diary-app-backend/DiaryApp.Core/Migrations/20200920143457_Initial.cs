using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiaryApp.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommonLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EventLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListItems_CommonLists_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "CommonLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(type: "int", nullable: false)
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MainPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainPages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MainPages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthPages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MonthPages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekPages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeekPages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportantEventsAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportantEventsID = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantEventsAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImportantEventsAreas_EventLists_ImportantEventsID",
                        column: x => x.ImportantEventsID,
                        principalTable: "EventLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportantEventsAreas_MainPages_PageID",
                        column: x => x.PageID,
                        principalTable: "MainPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportantThingsAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportantThingsID = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantThingsAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImportantThingsAreas_MainPages_PageID",
                        column: x => x.PageID,
                        principalTable: "MainPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportantThingsAreas_TodoLists_ImportantThingsID",
                        column: x => x.ImportantThingsID,
                        principalTable: "TodoLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesiresAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiresAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesiresAreas_MonthPages_PageID",
                        column: x => x.PageID,
                        principalTable: "MonthPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalsAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalsAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GoalsAreas_MonthPages_PageID",
                        column: x => x.PageID,
                        principalTable: "MonthPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdeasAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeasAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IdeasAreas_MonthPages_PageID",
                        column: x => x.PageID,
                        principalTable: "MonthPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasesAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasesAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchasesAreas_MonthPages_PageID",
                        column: x => x.PageID,
                        principalTable: "MonthPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotesAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotesAreas_WeekPages_PageID",
                        column: x => x.PageID,
                        principalTable: "WeekPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekPlansAreas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPlansAreas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeekPlansAreas_WeekPages_PageID",
                        column: x => x.PageID,
                        principalTable: "WeekPages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesiresLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListID = table.Column<int>(type: "int", nullable: false),
                    DesiresAreaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiresLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesiresLists_CommonLists_ListID",
                        column: x => x.ListID,
                        principalTable: "CommonLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesiresLists_DesiresAreas_DesiresAreaID",
                        column: x => x.DesiresAreaID,
                        principalTable: "DesiresAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitsTrackers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SelectedDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalsAreaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitsTrackers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HabitsTrackers_GoalsAreas_GoalsAreaID",
                        column: x => x.GoalsAreaID,
                        principalTable: "GoalsAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdeasLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListID = table.Column<int>(type: "int", nullable: false),
                    IdeasAreaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeasLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IdeasLists_CommonLists_ListID",
                        column: x => x.ListID,
                        principalTable: "CommonLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeasLists_IdeasAreas_IdeasAreaID",
                        column: x => x.IdeasAreaID,
                        principalTable: "IdeasAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasesLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListID = table.Column<int>(type: "int", nullable: false),
                    PurchasesAreaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasesLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchasesLists_PurchasesAreas_PurchasesAreaID",
                        column: x => x.PurchasesAreaID,
                        principalTable: "PurchasesAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasesLists_TodoLists_ListID",
                        column: x => x.ListID,
                        principalTable: "TodoLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(type: "date", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekPlansAreaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeekDays_WeekPlansAreas_WeekPlansAreaID",
                        column: x => x.WeekPlansAreaID,
                        principalTable: "WeekPlansAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeekPlansLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListID = table.Column<int>(type: "int", nullable: false),
                    WeekPlansAreaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekPlansLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeekPlansLists_TodoLists_ListID",
                        column: x => x.ListID,
                        principalTable: "TodoLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeekPlansLists_WeekPlansAreas_WeekPlansAreaID",
                        column: x => x.WeekPlansAreaID,
                        principalTable: "WeekPlansAreas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesiresAreas_PageID",
                table: "DesiresAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesiresLists_DesiresAreaID",
                table: "DesiresLists",
                column: "DesiresAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_DesiresLists_ListID",
                table: "DesiresLists",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerID",
                table: "Events",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_GoalsAreas_PageID",
                table: "GoalsAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HabitsTrackers_GoalsAreaID",
                table: "HabitsTrackers",
                column: "GoalsAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_IdeasAreas_PageID",
                table: "IdeasAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdeasLists_IdeasAreaID",
                table: "IdeasLists",
                column: "IdeasAreaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdeasLists_ListID",
                table: "IdeasLists",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantEventsAreas_ImportantEventsID",
                table: "ImportantEventsAreas",
                column: "ImportantEventsID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantEventsAreas_PageID",
                table: "ImportantEventsAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportantThingsAreas_ImportantThingsID",
                table: "ImportantThingsAreas",
                column: "ImportantThingsID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantThingsAreas_PageID",
                table: "ImportantThingsAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_OwnerID",
                table: "ListItems",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_MainPages_UserID",
                table: "MainPages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MonthPages_UserID",
                table: "MonthPages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotesAreas_PageID",
                table: "NotesAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesAreas_PageID",
                table: "PurchasesAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesLists_ListID",
                table: "PurchasesLists",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesLists_PurchasesAreaID",
                table: "PurchasesLists",
                column: "PurchasesAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_OwnerID",
                table: "Todos",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_WeekDays_WeekPlansAreaID",
                table: "WeekDays",
                column: "WeekPlansAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_WeekPages_UserID",
                table: "WeekPages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WeekPlansAreas_PageID",
                table: "WeekPlansAreas",
                column: "PageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WeekPlansLists_ListID",
                table: "WeekPlansLists",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_WeekPlansLists_WeekPlansAreaID",
                table: "WeekPlansLists",
                column: "WeekPlansAreaID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesiresLists");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "HabitsTrackers");

            migrationBuilder.DropTable(
                name: "IdeasLists");

            migrationBuilder.DropTable(
                name: "ImportantEventsAreas");

            migrationBuilder.DropTable(
                name: "ImportantThingsAreas");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "NotesAreas");

            migrationBuilder.DropTable(
                name: "PurchasesLists");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "WeekDays");

            migrationBuilder.DropTable(
                name: "WeekPlansLists");

            migrationBuilder.DropTable(
                name: "DesiresAreas");

            migrationBuilder.DropTable(
                name: "GoalsAreas");

            migrationBuilder.DropTable(
                name: "IdeasAreas");

            migrationBuilder.DropTable(
                name: "EventLists");

            migrationBuilder.DropTable(
                name: "MainPages");

            migrationBuilder.DropTable(
                name: "CommonLists");

            migrationBuilder.DropTable(
                name: "PurchasesAreas");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "WeekPlansAreas");

            migrationBuilder.DropTable(
                name: "MonthPages");

            migrationBuilder.DropTable(
                name: "WeekPages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
