using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serdiuk.API.Migrations
{
    public partial class AppUserFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_ApplicationUserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ApplicationUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "ApplicationUserBook",
                columns: table => new
                {
                    LikedBooksId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LikedUsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserBook", x => new { x.LikedBooksId, x.LikedUsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook_AspNetUsers_LikedUsersId",
                        column: x => x.LikedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserBook_Books_LikedBooksId",
                        column: x => x.LikedBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserBook_LikedUsersId",
                table: "ApplicationUserBook",
                column: "LikedUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserBook");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ApplicationUserId",
                table: "Books",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_ApplicationUserId",
                table: "Books",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
