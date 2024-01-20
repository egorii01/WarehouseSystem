using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckIDToEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckEntry_Check_CheckId",
                table: "CheckEntry");

            migrationBuilder.AlterColumn<int>(
                name: "CheckId",
                table: "CheckEntry",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckEntry_Check_CheckId",
                table: "CheckEntry",
                column: "CheckId",
                principalTable: "Check",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckEntry_Check_CheckId",
                table: "CheckEntry");

            migrationBuilder.AlterColumn<int>(
                name: "CheckId",
                table: "CheckEntry",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckEntry_Check_CheckId",
                table: "CheckEntry",
                column: "CheckId",
                principalTable: "Check",
                principalColumn: "Id");
        }
    }
}
