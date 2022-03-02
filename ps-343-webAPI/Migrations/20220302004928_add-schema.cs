using Microsoft.EntityFrameworkCore.Migrations;

namespace ps_343_webAPI.Migrations
{
    public partial class addschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ps-343");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Courses",
                newSchema: "ps-343");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Authors",
                newSchema: "ps-343");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "ps-343",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "Authors",
                schema: "ps-343",
                newName: "Authors");
        }
    }
}
