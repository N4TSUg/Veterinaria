using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Veterinaria.Migrations
{
    /// <inheritdoc />
    public partial class AddControlAntiparasitario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlesAntiparasitarios",
                columns: table => new
                {
                    IdControlAntiparasitario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMascota = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Peso = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Medicamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Dosis = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlesAntiparasitarios", x => x.IdControlAntiparasitario);
                    table.ForeignKey(
                        name: "FK_ControlesAntiparasitarios_Mascotas_IdMascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascotas",
                        principalColumn: "IdMascota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlesAntiparasitarios_IdMascota",
                table: "ControlesAntiparasitarios",
                column: "IdMascota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlesAntiparasitarios");
        }
    }
}
