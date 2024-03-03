using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class Note_Comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NoteComments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteComments_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteComments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NoteComments_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_StudentId",
                table: "Notes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TeacherId",
                table: "Notes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteComments_CreatedBy",
                table: "NoteComments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NoteComments_ModifiedBy",
                table: "NoteComments",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NoteComments_NoteId",
                table: "NoteComments",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_StudentId",
                table: "Notes",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_TeacherId",
                table: "Notes",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_StudentId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_TeacherId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "NoteComments");

            migrationBuilder.DropIndex(
                name: "IX_Notes_StudentId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_TeacherId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Notes");
        }
    }
}
