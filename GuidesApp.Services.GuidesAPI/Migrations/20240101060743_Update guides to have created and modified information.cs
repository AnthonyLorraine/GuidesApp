using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuidesApp.Services.GuidesAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updateguidestohavecreatedandmodifiedinformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Guides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Guides",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Guides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDateTime",
                table: "Guides",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "LastModifiedDateTime",
                table: "Guides");
        }
    }
}
