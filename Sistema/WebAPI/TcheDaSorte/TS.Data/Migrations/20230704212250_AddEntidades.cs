using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TS.Data.Migrations
{
    public partial class AddEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Premio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    DataEnvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ValorPremio = table.Column<double>(type: "float", nullable: false),
                    PrimeiroNumero = table.Column<int>(type: "int", nullable: false),
                    SegundoNumero = table.Column<int>(type: "int", nullable: false),
                    TerceiroNumero = table.Column<int>(type: "int", nullable: false),
                    QuartoNumero = table.Column<int>(type: "int", nullable: false),
                    QuintoNumero = table.Column<int>(type: "int", nullable: false),
                    SextoNumero = table.Column<int>(type: "int", nullable: false),
                    SetimoNumero = table.Column<int>(type: "int", nullable: false),
                    OitavoNumero = table.Column<int>(type: "int", nullable: false),
                    NonoNumero = table.Column<int>(type: "int", nullable: false),
                    DecimoNumero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    IndentityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cartela",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    PremioId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    Sorteada = table.Column<bool>(type: "bit", nullable: false),
                    CompraAprovada = table.Column<bool>(type: "bit", nullable: false),
                    PrimeiroNumero = table.Column<int>(type: "int", nullable: false),
                    SegundoNumero = table.Column<int>(type: "int", nullable: false),
                    TerceiroNumero = table.Column<int>(type: "int", nullable: false),
                    QuartoNumero = table.Column<int>(type: "int", nullable: false),
                    QuintoNumero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cartela_Premio_PremioId",
                        column: x => x.PremioId,
                        principalTable: "Premio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cartela_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cartela_PremioId",
                table: "Cartela",
                column: "PremioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartela_UsuarioId",
                table: "Cartela",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cartela");

            migrationBuilder.DropTable(
                name: "Premio");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
