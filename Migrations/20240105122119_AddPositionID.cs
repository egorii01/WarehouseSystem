using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Employee",
                newName: "PositionID");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_PositionId",
                table: "Employee",
                newName: "IX_Employee_PositionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Position_PositionID",
                table: "Employee",
                column: "PositionID",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Position_PositionID",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "PositionID",
                table: "Employee",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_PositionID",
                table: "Employee",
                newName: "IX_Employee_PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
