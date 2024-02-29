using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ABPBackendTZ.Migrations
{
    /// <inheritdoc />
    public partial class ReseedDb : Migration
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
                name: "PricesToShow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percentage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricesToShow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ButtonColorId = table.Column<int>(type: "int", nullable: false),
                    PriceToShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_ButtonColors_ButtonColorId",
                        column: x => x.ButtonColorId,
                        principalTable: "ButtonColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_PricesToShow_PriceToShowId",
                        column: x => x.PriceToShowId,
                        principalTable: "PricesToShow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "PricesToShow",
                columns: new[] { "Id", "Percentage", "Value" },
                values: new object[,]
                {
                    { 1, 0.75f, 10m },
                    { 2, 0.1f, 20m },
                    { 3, 0.05f, 50m },
                    { 4, 0.1f, 5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ButtonColorId",
                table: "Devices",
                column: "ButtonColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PriceToShowId",
                table: "Devices",
                column: "PriceToShowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "ButtonColors");

            migrationBuilder.DropTable(
                name: "PricesToShow");
        }
    }
}
