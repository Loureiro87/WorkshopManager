using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueReferenciaToPieza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitaPiezas_Pieza_PiezaId",
                table: "CitaPiezas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pieza",
                table: "Pieza");

            migrationBuilder.RenameTable(
                name: "Pieza",
                newName: "Piezas");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Piezas",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Piezas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Piezas",
                table: "Piezas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Piezas_Referencia",
                table: "Piezas",
                column: "Referencia",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CitaPiezas_Piezas_PiezaId",
                table: "CitaPiezas",
                column: "PiezaId",
                principalTable: "Piezas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitaPiezas_Piezas_PiezaId",
                table: "CitaPiezas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Piezas",
                table: "Piezas");

            migrationBuilder.DropIndex(
                name: "IX_Piezas_Referencia",
                table: "Piezas");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Piezas");

            migrationBuilder.RenameTable(
                name: "Piezas",
                newName: "Pieza");

            migrationBuilder.AlterColumn<string>(
                name: "Precio",
                table: "Pieza",
                type: "nvarchar(max)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pieza",
                table: "Pieza",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CitaPiezas_Pieza_PiezaId",
                table: "CitaPiezas",
                column: "PiezaId",
                principalTable: "Pieza",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
