using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yu_Gi_Oh_Database.Migrations
{
    /// <inheritdoc />
    public partial class CardsOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonsterCards_Cards_CardId",
                table: "MonsterCards");

            migrationBuilder.DropIndex(
                name: "IX_MonsterCards_CardId",
                table: "MonsterCards");

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "MonsterCards",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_MonsterCards_CardId",
                table: "MonsterCards",
                column: "CardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MonsterCards_Cards_CardId",
                table: "MonsterCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonsterCards_Cards_CardId",
                table: "MonsterCards");

            migrationBuilder.DropIndex(
                name: "IX_MonsterCards_CardId",
                table: "MonsterCards");

            migrationBuilder.AlterColumn<string>(
                name: "CardId",
                table: "MonsterCards",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonsterCards_CardId",
                table: "MonsterCards",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonsterCards_Cards_CardId",
                table: "MonsterCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
