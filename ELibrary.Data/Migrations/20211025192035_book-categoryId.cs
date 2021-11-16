using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELibrary.Data.Migrations;

public partial class bookcategoryId : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books");

        migrationBuilder.AlterColumn<int>(
            name: "CategoryId",
            table: "Books",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AddForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books");

        migrationBuilder.AlterColumn<int>(
            name: "CategoryId",
            table: "Books",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
