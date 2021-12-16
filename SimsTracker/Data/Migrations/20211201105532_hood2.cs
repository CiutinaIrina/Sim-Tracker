using Microsoft.EntityFrameworkCore.Migrations;

namespace SimsTracker.Data.Migrations
{
    public partial class hood2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sim_Hood_Hoodid",
                table: "Sim");

            migrationBuilder.DropColumn(
                name: "neighborhood",
                table: "Sim");

            migrationBuilder.RenameColumn(
                name: "Hoodid",
                table: "Sim",
                newName: "hoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Sim_Hoodid",
                table: "Sim",
                newName: "IX_Sim_hoodId");

            migrationBuilder.AlterColumn<int>(
                name: "hoodId",
                table: "Sim",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sim_Hood_hoodId",
                table: "Sim",
                column: "hoodId",
                principalTable: "Hood",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sim_Hood_hoodId",
                table: "Sim");

            migrationBuilder.RenameColumn(
                name: "hoodId",
                table: "Sim",
                newName: "Hoodid");

            migrationBuilder.RenameIndex(
                name: "IX_Sim_hoodId",
                table: "Sim",
                newName: "IX_Sim_Hoodid");

            migrationBuilder.AlterColumn<int>(
                name: "Hoodid",
                table: "Sim",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "neighborhood",
                table: "Sim",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sim_Hood_Hoodid",
                table: "Sim",
                column: "Hoodid",
                principalTable: "Hood",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
