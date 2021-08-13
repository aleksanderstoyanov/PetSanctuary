using Microsoft.EntityFrameworkCore.Migrations;

namespace PetSanctuary.Data.Migrations
{
    public partial class InitialAddedCollectionsForLikesDislikesIntoVet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Vets");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Vets");

            migrationBuilder.AddColumn<string>(
                name: "VetId",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VetId",
                table: "Dislikes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_VetId",
                table: "Likes",
                column: "VetId");

            migrationBuilder.CreateIndex(
                name: "IX_Dislikes_VetId",
                table: "Dislikes",
                column: "VetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dislikes_Vets_VetId",
                table: "Dislikes",
                column: "VetId",
                principalTable: "Vets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Vets_VetId",
                table: "Likes",
                column: "VetId",
                principalTable: "Vets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dislikes_Vets_VetId",
                table: "Dislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Vets_VetId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_VetId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Dislikes_VetId",
                table: "Dislikes");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Dislikes");

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
    }
}
