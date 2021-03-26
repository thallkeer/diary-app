using Microsoft.EntityFrameworkCore.Migrations;

namespace DiaryApp.Core.Migrations
{
    public partial class Eventlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesireLists_DesiresAreas_AreaOwnerID",
                table: "DesireLists");

            migrationBuilder.DropForeignKey(
                name: "FK_HabitTrackers_GoalsAreas_GoalsAreaID",
                table: "HabitTrackers");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaLists_IdeasAreas_AreaOwnerID",
                table: "IdeaLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportantEventsAreas_EventLists_ImportantEventsID",
                table: "ImportantEventsAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseLists_PurchasesAreas_AreaOwnerID",
                table: "PurchaseLists");

            migrationBuilder.RenameColumn(
                name: "AreaOwnerID",
                table: "PurchaseLists",
                newName: "AreaOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseLists_AreaOwnerID",
                table: "PurchaseLists",
                newName: "IX_PurchaseLists_AreaOwnerId");

            migrationBuilder.RenameColumn(
                name: "ImportantEventsID",
                table: "ImportantEventsAreas",
                newName: "ImportantEventsId");

            migrationBuilder.RenameIndex(
                name: "IX_ImportantEventsAreas_ImportantEventsID",
                table: "ImportantEventsAreas",
                newName: "IX_ImportantEventsAreas_ImportantEventsId");

            migrationBuilder.RenameColumn(
                name: "AreaOwnerID",
                table: "IdeaLists",
                newName: "AreaOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_IdeaLists_AreaOwnerID",
                table: "IdeaLists",
                newName: "IX_IdeaLists_AreaOwnerId");

            migrationBuilder.RenameColumn(
                name: "GoalsAreaID",
                table: "HabitTrackers",
                newName: "GoalsAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_HabitTrackers_GoalsAreaID",
                table: "HabitTrackers",
                newName: "IX_HabitTrackers_GoalsAreaId");

            migrationBuilder.RenameColumn(
                name: "AreaOwnerID",
                table: "DesireLists",
                newName: "AreaOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_DesireLists_AreaOwnerID",
                table: "DesireLists",
                newName: "IX_DesireLists_AreaOwnerId");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DesireLists_DesiresAreas_AreaOwnerId",
                table: "DesireLists",
                column: "AreaOwnerId",
                principalTable: "DesiresAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HabitTrackers_GoalsAreas_GoalsAreaId",
                table: "HabitTrackers",
                column: "GoalsAreaId",
                principalTable: "GoalsAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaLists_IdeasAreas_AreaOwnerId",
                table: "IdeaLists",
                column: "AreaOwnerId",
                principalTable: "IdeasAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportantEventsAreas_EventLists_ImportantEventsId",
                table: "ImportantEventsAreas",
                column: "ImportantEventsId",
                principalTable: "EventLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseLists_PurchasesAreas_AreaOwnerId",
                table: "PurchaseLists",
                column: "AreaOwnerId",
                principalTable: "PurchasesAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesireLists_DesiresAreas_AreaOwnerId",
                table: "DesireLists");

            migrationBuilder.DropForeignKey(
                name: "FK_HabitTrackers_GoalsAreas_GoalsAreaId",
                table: "HabitTrackers");

            migrationBuilder.DropForeignKey(
                name: "FK_IdeaLists_IdeasAreas_AreaOwnerId",
                table: "IdeaLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportantEventsAreas_EventLists_ImportantEventsId",
                table: "ImportantEventsAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseLists_PurchasesAreas_AreaOwnerId",
                table: "PurchaseLists");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "AreaOwnerId",
                table: "PurchaseLists",
                newName: "AreaOwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseLists_AreaOwnerId",
                table: "PurchaseLists",
                newName: "IX_PurchaseLists_AreaOwnerID");

            migrationBuilder.RenameColumn(
                name: "ImportantEventsId",
                table: "ImportantEventsAreas",
                newName: "ImportantEventsID");

            migrationBuilder.RenameIndex(
                name: "IX_ImportantEventsAreas_ImportantEventsId",
                table: "ImportantEventsAreas",
                newName: "IX_ImportantEventsAreas_ImportantEventsID");

            migrationBuilder.RenameColumn(
                name: "AreaOwnerId",
                table: "IdeaLists",
                newName: "AreaOwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_IdeaLists_AreaOwnerId",
                table: "IdeaLists",
                newName: "IX_IdeaLists_AreaOwnerID");

            migrationBuilder.RenameColumn(
                name: "GoalsAreaId",
                table: "HabitTrackers",
                newName: "GoalsAreaID");

            migrationBuilder.RenameIndex(
                name: "IX_HabitTrackers_GoalsAreaId",
                table: "HabitTrackers",
                newName: "IX_HabitTrackers_GoalsAreaID");

            migrationBuilder.RenameColumn(
                name: "AreaOwnerId",
                table: "DesireLists",
                newName: "AreaOwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_DesireLists_AreaOwnerId",
                table: "DesireLists",
                newName: "IX_DesireLists_AreaOwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_DesireLists_DesiresAreas_AreaOwnerID",
                table: "DesireLists",
                column: "AreaOwnerID",
                principalTable: "DesiresAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HabitTrackers_GoalsAreas_GoalsAreaID",
                table: "HabitTrackers",
                column: "GoalsAreaID",
                principalTable: "GoalsAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdeaLists_IdeasAreas_AreaOwnerID",
                table: "IdeaLists",
                column: "AreaOwnerID",
                principalTable: "IdeasAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportantEventsAreas_EventLists_ImportantEventsID",
                table: "ImportantEventsAreas",
                column: "ImportantEventsID",
                principalTable: "EventLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseLists_PurchasesAreas_AreaOwnerID",
                table: "PurchaseLists",
                column: "AreaOwnerID",
                principalTable: "PurchasesAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
