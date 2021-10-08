using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class DbSetEnergyRessource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergyRessource_AspNetUsers_ConsumerId",
                table: "EnergyRessource");

            migrationBuilder.DropForeignKey(
                name: "FK_EnergyRessource_AspNetUsers_ProducerId",
                table: "EnergyRessource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnergyRessource",
                table: "EnergyRessource");

            migrationBuilder.RenameTable(
                name: "EnergyRessource",
                newName: "EnergyRessources");

            migrationBuilder.RenameIndex(
                name: "IX_EnergyRessource_ProducerId",
                table: "EnergyRessources",
                newName: "IX_EnergyRessources_ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergyRessource_ConsumerId",
                table: "EnergyRessources",
                newName: "IX_EnergyRessources_ConsumerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnergyRessources",
                table: "EnergyRessources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyRessources_AspNetUsers_ConsumerId",
                table: "EnergyRessources",
                column: "ConsumerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyRessources_AspNetUsers_ProducerId",
                table: "EnergyRessources",
                column: "ProducerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergyRessources_AspNetUsers_ConsumerId",
                table: "EnergyRessources");

            migrationBuilder.DropForeignKey(
                name: "FK_EnergyRessources_AspNetUsers_ProducerId",
                table: "EnergyRessources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnergyRessources",
                table: "EnergyRessources");

            migrationBuilder.RenameTable(
                name: "EnergyRessources",
                newName: "EnergyRessource");

            migrationBuilder.RenameIndex(
                name: "IX_EnergyRessources_ProducerId",
                table: "EnergyRessource",
                newName: "IX_EnergyRessource_ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_EnergyRessources_ConsumerId",
                table: "EnergyRessource",
                newName: "IX_EnergyRessource_ConsumerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnergyRessource",
                table: "EnergyRessource",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyRessource_AspNetUsers_ConsumerId",
                table: "EnergyRessource",
                column: "ConsumerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyRessource_AspNetUsers_ProducerId",
                table: "EnergyRessource",
                column: "ProducerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
