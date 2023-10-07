using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudokuRecord.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class MatMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Matrix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StandardFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmitFinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mats", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mats");
        }
    }
}
