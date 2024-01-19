using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseSystem.Migrations
{
    /// <inheritdoc />
    public partial class checkentriesedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Check_Employee_CashierId",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckEntry_Product_ProductId",
                table: "CheckEntry");

            migrationBuilder.DropColumn(
                name: "GeneralAmount",
                table: "Check");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CheckEntry",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_CheckEntry_ProductId",
                table: "CheckEntry",
                newName: "IX_CheckEntry_ProductID");

            migrationBuilder.RenameColumn(
                name: "CashierId",
                table: "Check",
                newName: "CashierID");

            migrationBuilder.RenameIndex(
                name: "IX_Check_CashierId",
                table: "Check",
                newName: "IX_Check_CashierID");

            migrationBuilder.AddForeignKey(
                name: "FK_Check_Employee_CashierID",
                table: "Check",
                column: "CashierID",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckEntry_Product_ProductID",
                table: "CheckEntry",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Check_Employee_CashierID",
                table: "Check");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckEntry_Product_ProductID",
                table: "CheckEntry");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "CheckEntry",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckEntry_ProductID",
                table: "CheckEntry",
                newName: "IX_CheckEntry_ProductId");

            migrationBuilder.RenameColumn(
                name: "CashierID",
                table: "Check",
                newName: "CashierId");

            migrationBuilder.RenameIndex(
                name: "IX_Check_CashierID",
                table: "Check",
                newName: "IX_Check_CashierId");

            migrationBuilder.AddColumn<decimal>(
                name: "GeneralAmount",
                table: "Check",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Check_Employee_CashierId",
                table: "Check",
                column: "CashierId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckEntry_Product_ProductId",
                table: "CheckEntry",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
