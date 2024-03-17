using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eRakija.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proizvodi_TipoviProizvoda_tipProizvodaId",
                table: "Proizvodi");

            migrationBuilder.DropColumn(
                name: "TipId",
                table: "Proizvodi");

            migrationBuilder.AlterColumn<int>(
                name: "tipProizvodaId",
                table: "Proizvodi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvodi_TipoviProizvoda_tipProizvodaId",
                table: "Proizvodi",
                column: "tipProizvodaId",
                principalTable: "TipoviProizvoda",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proizvodi_TipoviProizvoda_tipProizvodaId",
                table: "Proizvodi");

            migrationBuilder.AlterColumn<int>(
                name: "tipProizvodaId",
                table: "Proizvodi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipId",
                table: "Proizvodi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvodi_TipoviProizvoda_tipProizvodaId",
                table: "Proizvodi",
                column: "tipProizvodaId",
                principalTable: "TipoviProizvoda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
