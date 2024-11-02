using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social.Net.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNavigationalProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId1",
                table: "Passwords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passwords_ProfileId1",
                table: "Passwords",
                column: "ProfileId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passwords_Profiles_ProfileId1",
                table: "Passwords",
                column: "ProfileId1",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passwords_Profiles_ProfileId1",
                table: "Passwords");

            migrationBuilder.DropIndex(
                name: "IX_Passwords_ProfileId1",
                table: "Passwords");

            migrationBuilder.DropColumn(
                name: "ProfileId1",
                table: "Passwords");
        }
    }
}
