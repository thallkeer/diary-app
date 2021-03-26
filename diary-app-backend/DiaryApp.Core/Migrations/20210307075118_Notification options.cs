using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiaryApp.Core.Migrations
{
    public partial class Notificationoptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "NotifyAt",
                table: "NotificationSettings",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 10, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "NotifyDayBefore",
                table: "NotificationSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NotificationDate",
                table: "Notifications",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.Sql(@"
UPDATE public.""GoalsAreas""
SET ""Header""='Трекеры привычек'
WHERE ""Header"" <> 'Трекеры привычек';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifyAt",
                table: "NotificationSettings");

            migrationBuilder.DropColumn(
                name: "NotifyDayBefore",
                table: "NotificationSettings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NotificationDate",
                table: "Notifications",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
