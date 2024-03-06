using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzWorks.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Workers_CreatedBy",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_CreatedBy",
                table: "Experiences");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkerId",
                table: "Experiences",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_WorkerId",
                table: "Experiences",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Workers_WorkerId",
                table: "Experiences",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Workers_WorkerId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_WorkerId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Experiences");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CreatedBy",
                table: "Experiences",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Workers_CreatedBy",
                table: "Experiences",
                column: "CreatedBy",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
