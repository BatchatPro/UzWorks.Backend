using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzWorks.Identity.Migrations
{
    /// <inheritdoc />
    public partial class _005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistrcitName",
                table: "AspNetUsers",
                newName: "DistrictName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistrictName",
                table: "AspNetUsers",
                newName: "DistrcitName");
        }
    }
}
