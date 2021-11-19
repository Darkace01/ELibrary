#nullable disable

namespace ELibrary.Data.Migrations;

public partial class pdfurl : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "PdfUrl",
            table: "Books",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PdfUrl",
            table: "Books");
    }
}
