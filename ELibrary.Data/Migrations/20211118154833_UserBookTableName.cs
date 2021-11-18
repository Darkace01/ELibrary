using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELibrary.Data.Migrations
{
    public partial class UserBookTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers_UserId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Books_BookId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UserBook");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserId",
                table: "UserBook",
                newName: "IX_UserBook_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_UserId",
                table: "UserBook",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Books_BookId",
                table: "UserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UserId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Books_BookId",
                table: "UserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook");

            migrationBuilder.RenameTable(
                name: "UserBook",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_UserId",
                table: "User",
                newName: "IX_User_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers_UserId",
                table: "User",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Books_BookId",
                table: "User",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
