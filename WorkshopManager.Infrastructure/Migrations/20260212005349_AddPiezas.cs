using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkshopManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPiezas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pieza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<string>(type: "nvarchar(max)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CitaPiezas",
                columns: table => new
                {
                    CitaId = table.Column<int>(type: "int", nullable: false),
                    PiezaId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaPiezas", x => new { x.CitaId, x.PiezaId });
                    table.ForeignKey(
                        name: "FK_CitaPiezas_Citas_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Citas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitaPiezas_Pieza_PiezaId",
                        column: x => x.PiezaId,
                        principalTable: "Pieza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitaPiezas_PiezaId",
                table: "CitaPiezas",
                column: "PiezaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitaPiezas");

            migrationBuilder.DropTable(
                name: "Pieza");
        }
    }
}
