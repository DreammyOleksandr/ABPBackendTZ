using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABPBackendTZ.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDevicePropsToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_ButtonColors_ButtonColorId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_PricesToShow_PriceToShowId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "PriceToShowId",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ButtonColorId",
                table: "Devices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_ButtonColors_ButtonColorId",
                table: "Devices",
                column: "ButtonColorId",
                principalTable: "ButtonColors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_PricesToShow_PriceToShowId",
                table: "Devices",
                column: "PriceToShowId",
                principalTable: "PricesToShow",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_ButtonColors_ButtonColorId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_PricesToShow_PriceToShowId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "PriceToShowId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ButtonColorId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_ButtonColors_ButtonColorId",
                table: "Devices",
                column: "ButtonColorId",
                principalTable: "ButtonColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_PricesToShow_PriceToShowId",
                table: "Devices",
                column: "PriceToShowId",
                principalTable: "PricesToShow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
