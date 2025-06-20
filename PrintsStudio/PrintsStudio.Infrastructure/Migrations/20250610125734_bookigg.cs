using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintsStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bookigg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DesignerId = table.Column<int>(type: "int", nullable: false),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    budget = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
