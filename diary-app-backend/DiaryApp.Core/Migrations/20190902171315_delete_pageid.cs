using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiaryApp.Core.Migrations
{
    public partial class delete_pageid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_FirstPage_PageID",
                table: "Events");

            migrationBuilder.DropTable(
                name: "TodoItem");

            migrationBuilder.DropTable(
                name: "FirstPage");

            migrationBuilder.DropIndex(
                name: "IX_Events_PageID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PageID",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageID",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FirstPage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstPage", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TodoItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Done = table.Column<bool>(nullable: false),
                    PageID = table.Column<int>(nullable: false),
                    PageOwnerID = table.Column<int>(nullable: true),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TodoItem_FirstPage_PageOwnerID",
                        column: x => x.PageOwnerID,
                        principalTable: "FirstPage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_PageID",
                table: "Events",
                column: "PageID");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_PageOwnerID",
                table: "TodoItem",
                column: "PageOwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FirstPage_PageID",
                table: "Events",
                column: "PageID",
                principalTable: "FirstPage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
