using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmigoOculto.Migrations
{
    /// <inheritdoc />
    public partial class SorteioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sorteios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GrupoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipanteId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmigoOcultoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorteios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sorteios_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sorteios_Participantes_AmigoOcultoId",
                        column: x => x.AmigoOcultoId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sorteios_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sorteios_AmigoOcultoId",
                table: "Sorteios",
                column: "AmigoOcultoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sorteios_GrupoId",
                table: "Sorteios",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sorteios_ParticipanteId",
                table: "Sorteios",
                column: "ParticipanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sorteios");
        }
    }
}
