using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzWorks.Identity.Migrations
{
    /// <inheritdoc />
    public partial class _004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistrcitName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrcitName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegionName",
                table: "AspNetUsers");
        }
    }
}
