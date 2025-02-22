using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ViewCount",
                table: "Books",
                newName: "view_count");

            migrationBuilder.RenameColumn(
                name: "PublicationYear",
                table: "Books",
                newName: "publication_year");

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Books",
                newName: "author_name");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Books",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "author_name",
                table: "Books",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "view_count",
                table: "Books",
                newName: "ViewCount");

            migrationBuilder.RenameColumn(
                name: "publication_year",
                table: "Books",
                newName: "PublicationYear");

            migrationBuilder.RenameColumn(
                name: "author_name",
                table: "Books",
                newName: "AuthorName");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                table: "Books",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);
        }
    }
}
