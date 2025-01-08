using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleToDoApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class toDoTaskEnumUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "IsDailyRecurring",
                table: "ToDoTasks");

            migrationBuilder.AddColumn<string>(
                name: "RecurringInterval",
                table: "ToDoTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskStatus",
                table: "ToDoTasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecurringInterval",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "ToDoTasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ToDoTasks",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDailyRecurring",
                table: "ToDoTasks",
                type: "bit",
                nullable: true);
        }
    }
}
