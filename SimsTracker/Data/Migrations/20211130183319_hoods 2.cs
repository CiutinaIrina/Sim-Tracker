using Microsoft.EntityFrameworkCore.Migrations;

namespace SimsTracker.Data.Migrations
{
    public partial class hoods2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hoodid",
                table: "Sim",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sim_Hoodid",
                table: "Sim",
                column: "Hoodid");

            migrationBuilder.AddForeignKey(
                name: "FK_Sim_Hood_Hoodid",
                table: "Sim",
                column: "Hoodid",
                principalTable: "Hood",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sim_Hood_Hoodid",
                table: "Sim");

            migrationBuilder.DropIndex(
                name: "IX_Sim_Hoodid",
                table: "Sim");

            migrationBuilder.DropColumn(
                name: "Hoodid",
                table: "Sim");
        }
    }
}
