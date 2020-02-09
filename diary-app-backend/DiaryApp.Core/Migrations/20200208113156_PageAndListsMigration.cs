using Microsoft.EntityFrameworkCore.Migrations;

namespace DiaryApp.Core.Migrations
{
    public partial class PageAndListsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_EventLists_EventListID",
                table: "PageBase");

            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_TodoLists_TodoListID",
                table: "PageBase");

            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_Users_UserID",
                table: "PageBase");

            migrationBuilder.DropIndex(
                name: "IX_PageBase_EventListID",
                table: "PageBase");

            migrationBuilder.DropIndex(
                name: "IX_PageBase_TodoListID",
                table: "PageBase");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_HabitsTrackers_GoalName",
                table: "HabitsTrackers");

            migrationBuilder.DropColumn(
                name: "EventListID",
                table: "PageBase");

            migrationBuilder.DropColumn(
                name: "TodoListID",
                table: "PageBase");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Todos",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TodoLists",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "PageBase",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoalName",
                table: "HabitsTrackers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EventLists",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_Users_UserID",
                table: "PageBase",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageBase_Users_UserID",
                table: "PageBase");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Todos");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "TodoLists",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "PageBase",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "EventListID",
                table: "PageBase",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TodoListID",
                table: "PageBase",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoalName",
                table: "HabitsTrackers",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "EventLists",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_HabitsTrackers_GoalName",
                table: "HabitsTrackers",
                column: "GoalName");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_EventListID",
                table: "PageBase",
                column: "EventListID");

            migrationBuilder.CreateIndex(
                name: "IX_PageBase_TodoListID",
                table: "PageBase",
                column: "TodoListID");

            migrationBuilder.AddForeignKey(
                name: "FK_PageBase_EventLists_EventListID",
                table: "PageBase",
                column: "EventListID",
                principalTable: "EventLists",
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
                name: "FK_PageBase_Users_UserID",
                table: "PageBase",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
