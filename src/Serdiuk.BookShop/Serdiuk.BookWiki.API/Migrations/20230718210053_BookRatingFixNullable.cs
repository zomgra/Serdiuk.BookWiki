using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Serdiuk.API.Migrations
{
    public partial class BookRatingFixNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
