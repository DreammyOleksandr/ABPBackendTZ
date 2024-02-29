using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABPBackendTZ.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ButtonColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HEX = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ButtonColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percentage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceChanges", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ButtonColors",
                columns: new[] { "Id", "HEX" },
                values: new object[,]
                {
                    { 1, "#FF0000" },
                    { 2, "#00FF00" },
                    { 3, "#0000FF" }
                });

            migrationBuilder.InsertData(
                table: "PriceChanges",
                columns: new[] { "Id", "Percentage", "Value" },
                values: new object[,]
                {
                    { 1, 0.75f, 10m },
                    { 2, 0.1f, 20m },
                    { 3, 0.05f, 50m },
                    { 4, 0.1f, 5m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ButtonColors");

            migrationBuilder.DropTable(
                name: "PriceChanges");
        }
    }
}
