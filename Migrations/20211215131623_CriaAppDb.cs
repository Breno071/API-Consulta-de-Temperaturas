using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteTecnico_.NET.Migrations
{
    public partial class CriaAppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temps",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    temp = table.Column<double>(type: "float", nullable: false),
                    feels_like = table.Column<double>(type: "float", nullable: false),
                    temp_min = table.Column<double>(type: "float", nullable: false),
                    temp_max = table.Column<double>(type: "float", nullable: false),
                    pressure = table.Column<int>(type: "int", nullable: false),
                    humidity = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    hour = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temps", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temps");
        }
    }
}
