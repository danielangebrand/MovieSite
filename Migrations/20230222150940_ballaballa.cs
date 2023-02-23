using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class ballaballa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Movies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProducerId",
                table: "Actors_Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Movies_ProducerId",
                table: "Actors_Movies",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_Producers_ProducerId",
                table: "Actors_Movies",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_Producers_ProducerId",
                table: "Actors_Movies");

            migrationBuilder.DropIndex(
                name: "IX_Actors_Movies_ProducerId",
                table: "Actors_Movies");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "Actors_Movies");
        }
    }
}
