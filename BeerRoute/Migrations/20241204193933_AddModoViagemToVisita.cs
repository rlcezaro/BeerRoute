using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeerRoute.Migrations
{
    public partial class AddModoViagemToVisita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModoViagem",
                table: "Visita",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModoViagem",
                table: "Visita");
        }
    }
}
