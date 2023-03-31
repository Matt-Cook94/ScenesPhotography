using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogWebApp.Migrations
{
    /// <inheritdoc />
    public partial class PostModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogCategory",
                table: "Post",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogCategory",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Post");
        }
    }
}
