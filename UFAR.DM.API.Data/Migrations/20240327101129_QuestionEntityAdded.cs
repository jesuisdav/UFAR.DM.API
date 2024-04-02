using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFAR.DM.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuestionEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "QuestionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Synonym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Random1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Random2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Random3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    SectionEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionEntity_Sections_SectionEntityId",
                        column: x => x.SectionEntityId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionEntity_SectionEntityId",
                table: "QuestionEntity",
                column: "SectionEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "Words",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
