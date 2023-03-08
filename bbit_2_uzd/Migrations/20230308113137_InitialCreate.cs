using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bbit_2_uzd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
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

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false),
                    Floor = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfTenants = table.Column<int>(type: "INTEGER", nullable: false),
                    FullArea = table.Column<decimal>(type: "TEXT", nullable: false),
                    LivingArea = table.Column<decimal>(type: "TEXT", nullable: false),
                    HouseId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartments_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentTenant",
                columns: table => new
                {
                    TenantApartmentsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentTenant", x => new { x.TenantApartmentsId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_ApartmentTenant_Apartments_TenantApartmentsId",
                        column: x => x.TenantApartmentsId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentTenant_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "City", "Country", "Number", "PostalCode", "Street" },
                values: new object[,]
                {
                    { new Guid("03289cb6-6bdb-4176-8739-4b8cf3742412"), "Test", "TestaValsts", 1, "TV-0001", "Default" },
                    { new Guid("22203f2f-5a81-4463-a346-6d8191a6f59d"), "Jelgava", "Lavia", 40, "LV-3001", "Liela" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "DateOfBirth", "Email", "IsOwner", "Name", "PersonalCode", "PhoneNumber", "Surname" },
                values: new object[,]
                {
                    { new Guid("322976c9-48ed-4cb9-abe4-c9626869e11f"), new DateTime(1974, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anna.Tests@fake.com", false, "Anna", "000000-000002", "+371 00000002", "Tests" },
                    { new Guid("615f9588-e225-491e-b046-64b144e6a082"), new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", false, "Zane", "000000-000004", "+371 00000004", "Tests" },
                    { new Guid("a8a1b97c-ccda-4f61-8f71-22cb2cdf805c"), new DateTime(1972, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "janis.tests@fake.com", true, "Janis", "000000-000001", "+371 00000001", "Tests" },
                    { new Guid("ef500529-dbee-40f9-88f0-0f7b9db9fdfa"), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maris.tests@fake.com", false, "Māris", "000000-000003", "+371 00000003", "Tests" }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "Floor", "FullArea", "HouseId", "LivingArea", "Number", "NumberOfRooms", "NumberOfTenants" },
                values: new object[,]
                {
                    { new Guid("0cf265e7-a334-4922-96bf-ca636d23819b"), 1, 70m, new Guid("03289cb6-6bdb-4176-8739-4b8cf3742412"), 64.5m, 1, 3, 4 },
                    { new Guid("302284f4-b5d9-4699-acc1-f5d408f5e23b"), 3, 70m, new Guid("03289cb6-6bdb-4176-8739-4b8cf3742412"), 64.5m, 5, 1, 0 },
                    { new Guid("39f9537a-0fea-4a54-9aed-d8aa30e999ad"), 2, 70m, new Guid("03289cb6-6bdb-4176-8739-4b8cf3742412"), 64.5m, 3, 1, 0 },
                    { new Guid("d3a44494-e8fc-4c7d-ba7a-0a86d7120f7b"), 2, 70m, new Guid("03289cb6-6bdb-4176-8739-4b8cf3742412"), 64.5m, 4, 1, 0 },
                    { new Guid("f22762d2-a33c-4446-957c-8732ea6cc8c0"), 1, 70m, new Guid("03289cb6-6bdb-4176-8739-4b8cf3742412"), 64.5m, 2, 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_HouseId",
                table: "Apartments",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentTenant_TenantId",
                table: "ApartmentTenant",
                column: "TenantId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentTenant");

            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}