using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleToDoApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedAppUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ToDoTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ToDoTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ToDoTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_AppUserId",
                table: "ToDoTasks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTasks_AppUsers_AppUserId",
                table: "ToDoTasks",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTasks_AppUsers_AppUserId",
                table: "ToDoTasks");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_ToDoTasks_AppUserId",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ToDoTasks");
        }
    }
}
