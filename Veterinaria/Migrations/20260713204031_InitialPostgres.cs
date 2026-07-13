using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Veterinaria.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DNI = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Vacunas",
                columns: table => new
                {
                    IdVacuna = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacunas", x => x.IdVacuna);
                });

            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    IdMascota = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdCliente = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Especie = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Raza = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Sexo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Peso = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    FotoUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.IdMascota);
                    table.ForeignKey(
                        name: "FK_Mascotas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmailUser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordUser = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IdRol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    IdConsulta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMascota = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Sintomas = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Diagnostico = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Tratamiento = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FechaProximaCita = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AdjuntoUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.IdConsulta);
                    table.ForeignKey(
                        name: "FK_Consultas_Mascotas_IdMascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascotas",
                        principalColumn: "IdMascota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacunasAplicadas",
                columns: table => new
                {
                    IdVacunaAplicada = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdMascota = table.Column<int>(type: "integer", nullable: false),
                    IdVacuna = table.Column<int>(type: "integer", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaProximaDosis = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacunasAplicadas", x => x.IdVacunaAplicada);
                    table.ForeignKey(
                        name: "FK_VacunasAplicadas_Mascotas_IdMascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascotas",
                        principalColumn: "IdMascota",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacunasAplicadas_Vacunas_IdVacuna",
                        column: x => x.IdVacuna,
                        principalTable: "Vacunas",
                        principalColumn: "IdVacuna",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "NameRol" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Veterinario" }
                });

            migrationBuilder.InsertData(
                table: "Vacunas",
                columns: new[] { "IdVacuna", "Nombre", "Tipo" },
                values: new object[,]
                {
                    { 1, "Triple felina Rinotraqueitis Panleucopenia", "Nobivac" },
                    { 2, "Puppy DP", "Biocan" },
                    { 3, "Cuádruple DHPPI", "Nobivac" },
                    { 4, "Quíntuple DAPPv+L4", "Nobivac" },
                    { 5, "Rabia (Rabies)", "Nobivac" },
                    { 6, "Sextuple PDHLPiR", "Biocan" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUser", "EmailUser", "IdRol", "NameUser", "PasswordUser" },
                values: new object[,]
                {
                    { 1, "enriquearana1402@gmail.com", 1, "Administrador", "AQAAAAIAAYagAAAAEMGr17R7Y/RniLPqGvxDVm+wu1HJ+zO+blOyf+W61SXe3PmjMGjKzfLBaOrksbPaSw==" },
                    { 2, "vetnarro@gmail.com", 2, "Enrique Narro", "AQAAAAIAAYagAAAAELq7CIaD5kLrTogQkheXcq6zVVGqO+Za/pV1tM2GyBXozcCOMNiwp6jtFGL+YMyqhw==" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IdMascota",
                table: "Consultas",
                column: "IdMascota");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_IdCliente",
                table: "Mascotas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_VacunasAplicadas_IdMascota",
                table: "VacunasAplicadas",
                column: "IdMascota");

            migrationBuilder.CreateIndex(
                name: "IX_VacunasAplicadas_IdVacuna",
                table: "VacunasAplicadas",
                column: "IdVacuna");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "VacunasAplicadas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Mascotas");

            migrationBuilder.DropTable(
                name: "Vacunas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
