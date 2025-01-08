using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleToDoApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class authPropMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "AppUsers",
                newName: "OTP");

            migrationBuilder.AddColumn<bool>(
                name: "OTPConfirmed",
                table: "AppUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpiryDate",
                table: "AppUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTPConfirmed",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "OTPExpiryDate",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "OTP",
                table: "AppUsers",
                newName: "PasswordSalt");
        }
    }
}
