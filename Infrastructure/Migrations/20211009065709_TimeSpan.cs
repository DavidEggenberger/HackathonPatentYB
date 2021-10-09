using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class TimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "EnergyRessources");

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "EnergyRessources",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "EnergyRessources");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "EnergyRessources",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan());
        }
    }
}
