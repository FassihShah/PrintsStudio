using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintsStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Designers_DesignerProfileDesignerId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DesignerProfileDesignerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Designers_DesignerProfileDesignerId",
                table: "AspNetUsers",
                column: "DesignerProfileDesignerId",
                principalTable: "Designers",
                principalColumn: "DesignerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Designers_DesignerProfileDesignerId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DesignerProfileDesignerId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Designers_DesignerProfileDesignerId",
                table: "AspNetUsers",
                column: "DesignerProfileDesignerId",
                principalTable: "Designers",
                principalColumn: "DesignerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
