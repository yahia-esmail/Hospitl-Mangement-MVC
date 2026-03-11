using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospitl_Mangement_MVC.Migrations
{
    /// <inheritdoc />
    public partial class add_role_Staff2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_AspNetRoles_RoleId1",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_RoleId1",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "Staffs");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "Staffs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_RoleId",
                table: "Staffs",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_AspNetRoles_RoleId",
                table: "Staffs",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_AspNetRoles_RoleId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_RoleId",
                table: "Staffs");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Staffs",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "Staffs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_RoleId1",
                table: "Staffs",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_AspNetRoles_RoleId1",
                table: "Staffs",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
