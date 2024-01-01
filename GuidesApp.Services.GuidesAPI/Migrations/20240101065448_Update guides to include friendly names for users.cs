using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuidesApp.Services.GuidesAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updateguidestoincludefriendlynamesforusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByDisplayName",
                table: "Guides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedByDisplayName",
                table: "Guides",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByDisplayName",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "LastModifiedByDisplayName",
                table: "Guides");
        }
    }
}
