using Microsoft.EntityFrameworkCore.Migrations;

namespace PetSanctuary.Data.Migrations
{
    public partial class PetUpdatedColumnImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pets");
        }
    }
}
