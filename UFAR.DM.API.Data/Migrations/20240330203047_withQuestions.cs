using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UFAR.DM.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class withQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionEntity_Sections_SectionEntityId",
                table: "QuestionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionEntity",
                table: "QuestionEntity");

            migrationBuilder.RenameTable(
                name: "QuestionEntity",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionEntity_SectionEntityId",
                table: "Questions",
                newName: "IX_Questions_SectionEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Sections_SectionEntityId",
                table: "Questions",
                column: "SectionEntityId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Sections_SectionEntityId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "QuestionEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_SectionEntityId",
                table: "QuestionEntity",
                newName: "IX_QuestionEntity_SectionEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionEntity",
                table: "QuestionEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionEntity_Sections_SectionEntityId",
                table: "QuestionEntity",
                column: "SectionEntityId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
