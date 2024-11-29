using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerRoute.Migrations
{
    public partial class AddVisitaCervejaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Cervejaria_CervejariaId",
                table: "Visita");

            migrationBuilder.AlterColumn<int>(
                name: "CervejariaId",
                table: "Visita",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "VisitaCervejaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitaId = table.Column<int>(type: "int", nullable: false),
                    CervejariaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitaCervejaria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitaCervejaria_Cervejaria_CervejariaId",
                        column: x => x.CervejariaId,
                        principalTable: "Cervejaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitaCervejaria_Visita_VisitaId",
                        column: x => x.VisitaId,
                        principalTable: "Visita",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitaCervejaria_CervejariaId",
                table: "VisitaCervejaria",
                column: "CervejariaId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitaCervejaria_VisitaId",
                table: "VisitaCervejaria",
                column: "VisitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Cervejaria_CervejariaId",
                table: "Visita",
                column: "CervejariaId",
                principalTable: "Cervejaria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Cervejaria_CervejariaId",
                table: "Visita");

            migrationBuilder.DropTable(
                name: "VisitaCervejaria");

            migrationBuilder.AlterColumn<int>(
                name: "CervejariaId",
                table: "Visita",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Cervejaria_CervejariaId",
                table: "Visita",
                column: "CervejariaId",
                principalTable: "Cervejaria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
