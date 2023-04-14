using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class notetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "note",
                schema: "VmProjectBE",
                columns: table => new
                {
                    note_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    note = table.Column<string>(type: "varchar(max)", nullable: false),
                    section_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_note", x => x.note_id);
                    table.ForeignKey(
                        name: "FK_note_section_section_id",
                        column: x => x.section_id,
                        principalSchema: "VmProjectBE",
                        principalTable: "section",
                        principalColumn: "section_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_note_section_id",
                schema: "VmProjectBE",
                table: "note",
                column: "section_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "note",
                schema: "VmProjectBE");
        }
    }
}
