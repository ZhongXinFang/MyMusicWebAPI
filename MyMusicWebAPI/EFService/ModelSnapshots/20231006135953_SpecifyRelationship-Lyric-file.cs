using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMusicWebAPI.EFService.ModelSnapshots
{
    /// <inheritdoc />
    public partial class SpecifyRelationshipLyricfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lyricfilesjson",
                schema: "dbo",
                table: "Lyric",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lyricfilesjson",
                schema: "dbo",
                table: "Lyric");
        }
    }
}
