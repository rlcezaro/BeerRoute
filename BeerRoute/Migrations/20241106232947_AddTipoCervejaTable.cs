using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerRoute.Migrations
{
    public partial class AddTipoCervejaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoCerveja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estilo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fabricante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IBU = table.Column<int>(type: "int", nullable: false),
                    ABV = table.Column<double>(type: "float", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCerveja", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CervejariaTipoCerveja_TipoCervejaId",
                table: "CervejariaTipoCerveja",
                column: "TipoCervejaId");

            migrationBuilder.AddForeignKey(
                name: "FK_CervejariaTipoCerveja_TipoCerveja_TipoCervejaId",
                table: "CervejariaTipoCerveja",
                column: "TipoCervejaId",
                principalTable: "TipoCerveja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CervejariaTipoCerveja_TipoCerveja_TipoCervejaId",
                table: "CervejariaTipoCerveja");

            migrationBuilder.DropTable(
                name: "TipoCerveja");

            migrationBuilder.DropIndex(
                name: "IX_CervejariaTipoCerveja_TipoCervejaId",
                table: "CervejariaTipoCerveja");
        }
    }
}
