using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Questions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Questions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Polls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Polls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Polls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Answers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Answers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_DeletedById",
                table: "Questions",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_DeletedById",
                table: "Polls",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_DeletedById",
                table: "Answers",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AspNetUsers_DeletedById",
                table: "Answers",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_DeletedById",
                table: "Polls",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_DeletedById",
                table: "Questions",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AspNetUsers_DeletedById",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_DeletedById",
                table: "Polls");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_DeletedById",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_DeletedById",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Polls_DeletedById",
                table: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_Answers_DeletedById",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Answers");
        }
    }
}
