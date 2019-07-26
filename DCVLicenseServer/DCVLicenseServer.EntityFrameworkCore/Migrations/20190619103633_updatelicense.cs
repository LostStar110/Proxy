using Microsoft.EntityFrameworkCore.Migrations;

namespace DCVLicenseServer.EntityFrameworkCore.Migrations
{
    public partial class updatelicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZipFilePath",
                table: "ProductLicenses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipFilePath",
                table: "ProductLicenses");
        }
    }
}
