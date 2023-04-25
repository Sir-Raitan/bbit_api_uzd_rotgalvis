using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdServer.Data.Migrations.Identity.AppDb
{
    /// <inheritdoc />
    public partial class UpdatedUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResidentId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResidentId",
                table: "AspNetUsers");
        }
    }
}