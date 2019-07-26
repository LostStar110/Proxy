using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DCVLicenseServer.EntityFrameworkCore.Migrations
{
    public partial class addapilicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APILicenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Project = table.Column<string>(nullable: false),
                    Claimer = table.Column<string>(nullable: false),
                    StartTime = table.Column<int>(nullable: false),
                    EndTime = table.Column<int>(nullable: false),
                    TotalTime = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    AppId = table.Column<string>(nullable: false),
                    APIList = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileContent = table.Column<string>(nullable: true),
                    ZipFilePath = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    CreateUserIp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APILicenses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APILicenses");
        }
    }
}
