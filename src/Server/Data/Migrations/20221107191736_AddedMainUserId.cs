using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentManager.Server.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMainUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainUserId",
                table: "AspNetUsers");
        }
    }
}
