using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bbit_2_uzd.Migrations
{
    /// <inheritdoc />
    public partial class extra_indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //change the email to be nullable
            //tricky beacause apparently sqlite does not support alter column command
            migrationBuilder.Sql("PRAGMA foreign_keys = off;");

            migrationBuilder.CreateTable(
                name: "new_Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalCode = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    IsOwner = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO new_Tenants SELECT Id, Name, Surname, PersonalCode, DateOfBirth, PhoneNumber, Email, IsOwner FROM Tenants;");

            migrationBuilder.DropTable("Tenants");
            
            migrationBuilder.RenameTable(
                name: "new_Tenants",
                newName: "Tenants");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Email",
                table: "Tenants",
                column: "Email",
                unique: true,
                filter: "Email IS NOT NULL AND Email <> ''");

            migrationBuilder.CreateIndex(
               name: "IX_Tenants_PersonalCode",
               table: "Tenants",
               column: "PersonalCode",
               unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PhoneNumber",
                table: "Tenants",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.Sql("PRAGMA foreign_key_check;");

            migrationBuilder.Sql("PRAGMA foreign_keys=ON;");

            //create extra indexes.

            migrationBuilder.CreateIndex(
                name: "IX_Houses_Number_Street_City_Country",
                table: "Houses",
                columns: new[] { "Number", "Street", "City", "Country" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_Number_HouseId",
                table: "Apartments",
                columns: new[] { "Number", "HouseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_Email",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Houses_Number_Street_City_Country",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_Number_HouseId",
                table: "Apartments");

            //reset old table
            migrationBuilder.Sql("PRAGMA foreign_keys = off;");

            migrationBuilder.CreateTable(
                name: "new_Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalCode = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    IsOwner = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });
            migrationBuilder.Sql("INSERT INTO new_Tenants SELECT Id, Name, Surname, PersonalCode, DateOfBirth, PhoneNumber, Email, IsOwner FROM Tenants" +
                    "WHERE Email IS NOT NULL AND Email <> '';");//prevent insertion of invalid data

            migrationBuilder.DropTable("Tenants");

            migrationBuilder.RenameTable(
                name: "new_Tenants",
                newName: "Tenants");

            migrationBuilder.CreateIndex(
              name: "IX_Tenants_Email",
              table: "Tenants",
              column: "Email",
              unique: true);

            migrationBuilder.CreateIndex(
               name: "IX_Tenants_PersonalCode",
               table: "Tenants",
               column: "PersonalCode",
               unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PhoneNumber",
                table: "Tenants",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.Sql("PRAGMA foreign_key_check;");

            migrationBuilder.Sql("PRAGMA foreign_keys=ON;");
        }
    }
}
