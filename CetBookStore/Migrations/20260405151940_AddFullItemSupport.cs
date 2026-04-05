using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CetBookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddFullItemSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookTitle",
                table: "OrderItems",
                newName: "ItemName");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "OrderItems",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "OrderItems",
                newName: "BookTitle");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "OrderItems",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Books",
                newName: "Title");
        }
    }
}
