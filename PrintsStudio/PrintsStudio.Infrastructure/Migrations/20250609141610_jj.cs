using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintsStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class jj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomizationOptions_OrderItems_OrderItemId",
                table: "CustomizationOptions");

            migrationBuilder.DropIndex(
                name: "IX_CustomizationOptions_OrderItemId",
                table: "CustomizationOptions");

            migrationBuilder.DropColumn(
                name: "SelectedDesignTemplateId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "CustomizationOptions");

            migrationBuilder.AddColumn<string>(
                name: "AppliedCustomizations",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "SelectedDesignTemplateUrl",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppliedCustomizations",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SelectedDesignTemplateUrl",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "SelectedDesignTemplateId",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "CustomizationOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomizationOptions_OrderItemId",
                table: "CustomizationOptions",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomizationOptions_OrderItems_OrderItemId",
                table: "CustomizationOptions",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id");
        }
    }
}
