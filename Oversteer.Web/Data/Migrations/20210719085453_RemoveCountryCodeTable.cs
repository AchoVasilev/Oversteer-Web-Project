namespace Oversteer.Web.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveCountryCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CountryCodes_CountryCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_CountryCodes_CountryCodeId",
                table: "Countries");

            migrationBuilder.DropTable(
                name: "CountryCodes");

            migrationBuilder.DropIndex(
                name: "IX_Countries_CountryCodeId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CountryCodeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CountryCodeId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryCodeId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryCodeId",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryCodeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CountryCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsoCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryCodeId",
                table: "Countries",
                column: "CountryCodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryCodeId",
                table: "AspNetUsers",
                column: "CountryCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CountryCodes_CountryCodeId",
                table: "AspNetUsers",
                column: "CountryCodeId",
                principalTable: "CountryCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_CountryCodes_CountryCodeId",
                table: "Countries",
                column: "CountryCodeId",
                principalTable: "CountryCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
