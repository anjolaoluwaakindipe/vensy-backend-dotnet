using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vensy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeApplicationUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_companies_users_ApplicationUserId1",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fK_refreshTokens_users_ApplicationUserId1",
                table: "refreshTokens");

            migrationBuilder.DropIndex(
                name: "iX_refreshTokens_applicationUserId1",
                table: "refreshTokens");

            migrationBuilder.DropIndex(
                name: "iX_companies_applicationUserId1",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "applicationUserId1",
                table: "refreshTokens");

            migrationBuilder.DropColumn(
                name: "applicationUserId1",
                table: "companies");

            migrationBuilder.AlterColumn<string>(
                name: "applicationUserId",
                table: "refreshTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "applicationUserId",
                table: "companies",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "iX_refreshTokens_applicationUserId",
                table: "refreshTokens",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "iX_companies_applicationUserId",
                table: "companies",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "fK_companies_users_applicationUserId",
                table: "companies",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_refreshTokens_users_applicationUserId",
                table: "refreshTokens",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_companies_users_applicationUserId",
                table: "companies");

            migrationBuilder.DropForeignKey(
                name: "fK_refreshTokens_users_applicationUserId",
                table: "refreshTokens");

            migrationBuilder.DropIndex(
                name: "iX_refreshTokens_applicationUserId",
                table: "refreshTokens");

            migrationBuilder.DropIndex(
                name: "iX_companies_applicationUserId",
                table: "companies");

            migrationBuilder.AlterColumn<int>(
                name: "applicationUserId",
                table: "refreshTokens",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId1",
                table: "refreshTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "applicationUserId",
                table: "companies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId1",
                table: "companies",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "iX_refreshTokens_applicationUserId1",
                table: "refreshTokens",
                column: "applicationUserId1");

            migrationBuilder.CreateIndex(
                name: "iX_companies_applicationUserId1",
                table: "companies",
                column: "applicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "fK_companies_users_ApplicationUserId1",
                table: "companies",
                column: "applicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fK_refreshTokens_users_ApplicationUserId1",
                table: "refreshTokens",
                column: "applicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "id");
        }
    }
}
