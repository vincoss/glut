using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glut.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlutProject",
                columns: table => new
                {
                    GlutProjectId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GlutProjectName = table.Column<string>(nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    ModifiedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    CreatedByUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlutProject", x => x.GlutProjectId);
                });

            migrationBuilder.CreateTable(
                name: "GlutResultItem",
                columns: table => new
                {
                    GlutResultId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GlutProjectName = table.Column<string>(nullable: false),
                    GlutProjectRunId = table.Column<int>(nullable: false),
                    StartDateTimeUtc = table.Column<DateTime>(nullable: false),
                    EndDateTimeUtc = table.Column<DateTime>(nullable: false),
                    RequestUri = table.Column<string>(nullable: false),
                    IsSuccessStatusCode = table.Column<bool>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false),
                    HeaderLength = table.Column<long>(nullable: false),
                    ResponseLength = table.Column<long>(nullable: false),
                    TotalLegth = table.Column<long>(nullable: false),
                    RequestSentTicks = table.Column<long>(nullable: false),
                    ResponseTicks = table.Column<long>(nullable: false),
                    TotalTicks = table.Column<long>(nullable: false),
                    ResponseHeaders = table.Column<string>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    CreatedDateTimeUtc = table.Column<DateTime>(nullable: false),
                    CreatedByUserName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlutResultItem", x => x.GlutResultId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlutProject");

            migrationBuilder.DropTable(
                name: "GlutResultItem");
        }
    }
}
