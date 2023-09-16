using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMusicWebAPI.EFService.ModelSnapshots
{
    /// <inheritdoc />
    public partial class InitialCreate2309151451 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Revision = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    CreatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: true),
                    Createtime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    UpdatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: true),
                    Updatetime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)",maxLength: 50,nullable: false),
                    Password = table.Column<string>(type: "varchar(500)",unicode: false,maxLength: 500,nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User",x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_User_CreatebyUserId",
                        column: x => x.CreatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_UpdatebyUserId",
                        column: x => x.UpdatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "用户表");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Revision = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    CreatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Createtime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    UpdatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Updatetime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    Countrydescription = table.Column<string>(type: "nvarchar(max)",nullable: false),
                    Countrycode = table.Column<string>(type: "nvarchar(max)",nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country",x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_User_CreatebyUserId",
                        column: x => x.CreatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_User_UpdatebyUserId",
                        column: x => x.UpdatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "国家字典");

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Revision = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    CreatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Createtime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    UpdatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Updatetime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    Languagedescription = table.Column<string>(type: "nvarchar(max)",nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language",x => x.Id);
                    table.ForeignKey(
                        name: "FK_Language_User_CreatebyUserId",
                        column: x => x.CreatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Language_User_UpdatebyUserId",
                        column: x => x.UpdatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "语言字典");

            migrationBuilder.CreateTable(
                name: "Artist",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Revision = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    CreatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Createtime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    UpdatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Updatetime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)",nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)",nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)",nullable: false),
                    Dateofbirth = table.Column<DateTime>(type: "datetime2",nullable: false),
                    CountryofbirthCountryId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    NationalityCountryId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Gender = table.Column<int>(type: "int",nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)",nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist",x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artist_Country_CountryofbirthCountryId",
                        column: x => x.CountryofbirthCountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Artist_Country_NationalityCountryId",
                        column: x => x.NationalityCountryId,
                        principalSchema: "dbo",
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Artist_User_CreatebyUserId",
                        column: x => x.CreatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Artist_User_UpdatebyUserId",
                        column: x => x.UpdatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "艺术家信息表");

            migrationBuilder.CreateTable(
                name: "Lyric",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Revision = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    CreatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Createtime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    UpdatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Updatetime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    LyricistUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier",nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lyric",x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lyric_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "dbo",
                        principalTable: "Language",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lyric_User_CreatebyUserId",
                        column: x => x.CreatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lyric_User_LyricistUserId",
                        column: x => x.LyricistUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lyric_User_UpdatebyUserId",
                        column: x => x.UpdatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "歌词表");

            migrationBuilder.CreateTable(
                name: "Song",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Revision = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    CreatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Createtime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    UpdatebyUserId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Updatetime = table.Column<DateTime>(type: "datetime2",nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)",maxLength: 100,nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Album = table.Column<string>(type: "nvarchar(100)",maxLength: 100,nullable: false),
                    Publicationdate = table.Column<DateTime>(type: "datetime2",nullable: false),
                    ComposerArtistId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    LyricistArtistId = table.Column<Guid>(type: "uniqueidentifier",nullable: false),
                    Coverimgjson = table.Column<string>(type: "nvarchar(1000)",maxLength: 1000,nullable: false),
                    Backgroundimgjson = table.Column<string>(type: "nvarchar(1000)",maxLength: 1000,nullable: false),
                    Audiofilesjson = table.Column<string>(type: "nvarchar(1000)",maxLength: 1000,nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song",x => x.Id);
                    table.ForeignKey(
                        name: "FK_Song_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalSchema: "dbo",
                        principalTable: "Artist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_Artist_ComposerArtistId",
                        column: x => x.ComposerArtistId,
                        principalSchema: "dbo",
                        principalTable: "Artist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_Artist_LyricistArtistId",
                        column: x => x.LyricistArtistId,
                        principalSchema: "dbo",
                        principalTable: "Artist",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_User_CreatebyUserId",
                        column: x => x.CreatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Song_User_UpdatebyUserId",
                        column: x => x.UpdatebyUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                },
                comment: "歌曲信息表");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_CountryofbirthCountryId",
                schema: "dbo",
                table: "Artist",
                column: "CountryofbirthCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_CreatebyUserId",
                schema: "dbo",
                table: "Artist",
                column: "CreatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_NationalityCountryId",
                schema: "dbo",
                table: "Artist",
                column: "NationalityCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_UpdatebyUserId",
                schema: "dbo",
                table: "Artist",
                column: "UpdatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CreatebyUserId",
                schema: "dbo",
                table: "Country",
                column: "CreatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UpdatebyUserId",
                schema: "dbo",
                table: "Country",
                column: "UpdatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_CreatebyUserId",
                schema: "dbo",
                table: "Language",
                column: "CreatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_UpdatebyUserId",
                schema: "dbo",
                table: "Language",
                column: "UpdatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lyric_CreatebyUserId",
                schema: "dbo",
                table: "Lyric",
                column: "CreatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lyric_LanguageId",
                schema: "dbo",
                table: "Lyric",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Lyric_LyricistUserId",
                schema: "dbo",
                table: "Lyric",
                column: "LyricistUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lyric_UpdatebyUserId",
                schema: "dbo",
                table: "Lyric",
                column: "UpdatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_ArtistId",
                schema: "dbo",
                table: "Song",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_ComposerArtistId",
                schema: "dbo",
                table: "Song",
                column: "ComposerArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_CreatebyUserId",
                schema: "dbo",
                table: "Song",
                column: "CreatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_LyricistArtistId",
                schema: "dbo",
                table: "Song",
                column: "LyricistArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_UpdatebyUserId",
                schema: "dbo",
                table: "Song",
                column: "UpdatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatebyUserId",
                schema: "dbo",
                table: "User",
                column: "CreatebyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatebyUserId",
                schema: "dbo",
                table: "User",
                column: "UpdatebyUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lyric",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Song",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Artist",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
