﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExceptionHandling.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_UserName",
                table: "User");
        }
    }
}
