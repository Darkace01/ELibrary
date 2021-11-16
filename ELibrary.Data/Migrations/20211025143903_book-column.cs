using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELibrary.Data.Migrations;

public partial class bookcolumn : Migration
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
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Code",
            table: "Books",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "ImageUrl",
            table: "Books",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Name",
            table: "Books",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Tags",
            table: "Books",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetUserTokens",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserTokens",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "ProviderKey",
            table: "AspNetUserLogins",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserLogins",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AddForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books");

        migrationBuilder.DropColumn(
            name: "Code",
            table: "Books");

        migrationBuilder.DropColumn(
            name: "ImageUrl",
            table: "Books");

        migrationBuilder.DropColumn(
            name: "Name",
            table: "Books");

        migrationBuilder.DropColumn(
            name: "Tags",
            table: "Books");

        migrationBuilder.AlterColumn<int>(
            name: "CategoryId",
            table: "Books",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetUserTokens",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserTokens",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "ProviderKey",
            table: "AspNetUserLogins",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserLogins",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AddForeignKey(
            name: "FK_Books_Categories_CategoryId",
            table: "Books",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id");
    }
}
