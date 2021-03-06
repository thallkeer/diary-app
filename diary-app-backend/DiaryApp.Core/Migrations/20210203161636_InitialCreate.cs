﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DiaryApp.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommonLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListItems_CommonLists_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "CommonLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_EventLists_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "EventLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Done = table.Column<bool>(type: "boolean", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OwnerID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todos_TodoLists_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MainPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainPages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthPages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportantEventsAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImportantEventsID = table.Column<int>(type: "integer", nullable: false),
                    Header = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantEventsAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportantEventsAreas_EventLists_ImportantEventsID",
                        column: x => x.ImportantEventsID,
                        principalTable: "EventLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportantEventsAreas_MainPages_PageId",
                        column: x => x.PageId,
                        principalTable: "MainPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportantThingsAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImportantThingsID = table.Column<int>(type: "integer", nullable: false),
                    Header = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantThingsAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportantThingsAreas_MainPages_PageId",
                        column: x => x.PageId,
                        principalTable: "MainPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportantThingsAreas_TodoLists_ImportantThingsID",
                        column: x => x.ImportantThingsID,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesiresAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesiresAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesiresAreas_MonthPages_PageId",
                        column: x => x.PageId,
                        principalTable: "MonthPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoalsAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoalsAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoalsAreas_MonthPages_PageId",
                        column: x => x.PageId,
                        principalTable: "MonthPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdeasAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeasAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeasAreas_MonthPages_PageId",
                        column: x => x.PageId,
                        principalTable: "MonthPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasesAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Header = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasesAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasesAreas_MonthPages_PageId",
                        column: x => x.PageId,
                        principalTable: "MonthPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageAreaTransferSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserSettingsId = table.Column<int>(type: "integer", nullable: false),
                    TransferPurchasesArea = table.Column<bool>(type: "boolean", nullable: false),
                    TransferDesiresArea = table.Column<bool>(type: "boolean", nullable: false),
                    TransferIdeasArea = table.Column<bool>(type: "boolean", nullable: false),
                    TransferGoalsArea = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageAreaTransferSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageAreaTransferSettings_UserSettings_UserSettingsId",
                        column: x => x.UserSettingsId,
                        principalTable: "UserSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesireLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListID = table.Column<int>(type: "integer", nullable: false),
                    AreaOwnerID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesireLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesireLists_CommonLists_ListID",
                        column: x => x.ListID,
                        principalTable: "CommonLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesireLists_DesiresAreas_AreaOwnerID",
                        column: x => x.AreaOwnerID,
                        principalTable: "DesiresAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitTrackers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoalName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    GoalsAreaID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitTrackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitTrackers_GoalsAreas_GoalsAreaID",
                        column: x => x.GoalsAreaID,
                        principalTable: "GoalsAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdeaLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListID = table.Column<int>(type: "integer", nullable: false),
                    AreaOwnerID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdeaLists_CommonLists_ListID",
                        column: x => x.ListID,
                        principalTable: "CommonLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdeaLists_IdeasAreas_AreaOwnerID",
                        column: x => x.AreaOwnerID,
                        principalTable: "IdeasAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListID = table.Column<int>(type: "integer", nullable: false),
                    AreaOwnerID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseLists_PurchasesAreas_AreaOwnerID",
                        column: x => x.AreaOwnerID,
                        principalTable: "PurchasesAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseLists_TodoLists_ListID",
                        column: x => x.ListID,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    HabitTrackerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitDays_HabitTrackers_HabitTrackerId",
                        column: x => x.HabitTrackerId,
                        principalTable: "HabitTrackers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DesireLists_AreaOwnerID",
                table: "DesireLists",
                column: "AreaOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_DesireLists_ListID",
                table: "DesireLists",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_DesiresAreas_PageId",
                table: "DesiresAreas",
                column: "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_OwnerID",
                table: "Events",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_GoalsAreas_PageId",
                table: "GoalsAreas",
                column: "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HabitDays_HabitTrackerId",
                table: "HabitDays",
                column: "HabitTrackerId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitTrackers_GoalsAreaID",
                table: "HabitTrackers",
                column: "GoalsAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaLists_AreaOwnerID",
                table: "IdeaLists",
                column: "AreaOwnerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdeaLists_ListID",
                table: "IdeaLists",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_IdeasAreas_PageId",
                table: "IdeasAreas",
                column: "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportantEventsAreas_ImportantEventsID",
                table: "ImportantEventsAreas",
                column: "ImportantEventsID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantEventsAreas_PageId",
                table: "ImportantEventsAreas",
                column: "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportantThingsAreas_ImportantThingsID",
                table: "ImportantThingsAreas",
                column: "ImportantThingsID");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantThingsAreas_PageId",
                table: "ImportantThingsAreas",
                column: "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_OwnerID",
                table: "ListItems",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_MainPages_UserId_Year_Month",
                table: "MainPages",
                columns: new[] { "UserId", "Year", "Month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonthPages_UserId_Year_Month",
                table: "MonthPages",
                columns: new[] { "UserId", "Year", "Month" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageAreaTransferSettings_UserSettingsId",
                table: "PageAreaTransferSettings",
                column: "UserSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLists_AreaOwnerID",
                table: "PurchaseLists",
                column: "AreaOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLists_ListID",
                table: "PurchaseLists",
                column: "ListID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesAreas_PageId",
                table: "PurchasesAreas",
                column: "PageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_OwnerID",
                table: "Todos",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserId",
                table: "UserSettings",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesireLists");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "HabitDays");

            migrationBuilder.DropTable(
                name: "IdeaLists");

            migrationBuilder.DropTable(
                name: "ImportantEventsAreas");

            migrationBuilder.DropTable(
                name: "ImportantThingsAreas");

            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "PageAreaTransferSettings");

            migrationBuilder.DropTable(
                name: "PurchaseLists");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "DesiresAreas");

            migrationBuilder.DropTable(
                name: "HabitTrackers");

            migrationBuilder.DropTable(
                name: "IdeasAreas");

            migrationBuilder.DropTable(
                name: "EventLists");

            migrationBuilder.DropTable(
                name: "MainPages");

            migrationBuilder.DropTable(
                name: "CommonLists");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "PurchasesAreas");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "GoalsAreas");

            migrationBuilder.DropTable(
                name: "MonthPages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
