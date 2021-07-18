using Microsoft.EntityFrameworkCore.Migrations;

namespace PetSanctuary.Data.Migrations
{
    public partial class AddingLikeDislikeToVets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Vets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Vets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Vets");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Vets");
        }
    }
}
