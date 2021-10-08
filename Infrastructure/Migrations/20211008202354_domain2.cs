using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class domain2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionNight",
                table: "EnergyRessources",
                newName: "ProductionNightkWh");

            migrationBuilder.RenameColumn(
                name: "ProductionDaySunny",
                table: "EnergyRessources",
                newName: "ProductionDaySunnykWh");

            migrationBuilder.RenameColumn(
                name: "ProductionDayRainny",
                table: "EnergyRessources",
                newName: "ProductionDayRainnykWh");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "EnergyRessources",
                newName: "PricePerkWh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionNightkWh",
                table: "EnergyRessources",
                newName: "ProductionNight");

            migrationBuilder.RenameColumn(
                name: "ProductionDaySunnykWh",
                table: "EnergyRessources",
                newName: "ProductionDaySunny");

            migrationBuilder.RenameColumn(
                name: "ProductionDayRainnykWh",
                table: "EnergyRessources",
                newName: "ProductionDayRainny");

            migrationBuilder.RenameColumn(
                name: "PricePerkWh",
                table: "EnergyRessources",
                newName: "Price");
        }
    }
}
