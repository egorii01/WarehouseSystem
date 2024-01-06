using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseSystem.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationInvoicesAndImports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_Invoice_InvoiceId",
                table: "Import");

            migrationBuilder.DropForeignKey(
                name: "FK_Import_Product_ProductId",
                table: "Import");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Employee_ResponsibleId",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "ResponsibleId",
                table: "Invoice",
                newName: "ResponsibleID");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_ResponsibleId",
                table: "Invoice",
                newName: "IX_Invoice_ResponsibleID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Import",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "Import",
                newName: "InvoiceID");

            migrationBuilder.RenameIndex(
                name: "IX_Import_ProductId",
                table: "Import",
                newName: "IX_Import_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Import_InvoiceId",
                table: "Import",
                newName: "IX_Import_InvoiceID");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceID",
                table: "Import",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Invoice_InvoiceID",
                table: "Import",
                column: "InvoiceID",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Product_ProductID",
                table: "Import",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Employee_ResponsibleID",
                table: "Invoice",
                column: "ResponsibleID",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Import_Invoice_InvoiceID",
                table: "Import");

            migrationBuilder.DropForeignKey(
                name: "FK_Import_Product_ProductID",
                table: "Import");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Employee_ResponsibleID",
                table: "Invoice");

            migrationBuilder.RenameColumn(
                name: "ResponsibleID",
                table: "Invoice",
                newName: "ResponsibleId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_ResponsibleID",
                table: "Invoice",
                newName: "IX_Invoice_ResponsibleId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Import",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "InvoiceID",
                table: "Import",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Import_ProductID",
                table: "Import",
                newName: "IX_Import_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Import_InvoiceID",
                table: "Import",
                newName: "IX_Import_InvoiceId");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "Import",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Invoice_InvoiceId",
                table: "Import",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Import_Product_ProductId",
                table: "Import",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Employee_ResponsibleId",
                table: "Invoice",
                column: "ResponsibleId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
