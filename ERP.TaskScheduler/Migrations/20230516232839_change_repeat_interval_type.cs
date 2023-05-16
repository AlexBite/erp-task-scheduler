using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.TaskScheduler.Migrations
{
    /// <inheritdoc />
    public partial class change_repeat_interval_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "repeat_interval",
                table: "tasks");

            migrationBuilder.AddColumn<int>(
                name: "repeat_interval_minutes",
                table: "tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "task_id",
                table: "history",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_history_task_id",
                table: "history",
                column: "task_id");

            migrationBuilder.AddForeignKey(
                name: "fk_history_tasks_task_id",
                table: "history",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_history_tasks_task_id",
                table: "history");

            migrationBuilder.DropIndex(
                name: "ix_history_task_id",
                table: "history");

            migrationBuilder.DropColumn(
                name: "repeat_interval_minutes",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "task_id",
                table: "history");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "repeat_interval",
                table: "tasks",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
