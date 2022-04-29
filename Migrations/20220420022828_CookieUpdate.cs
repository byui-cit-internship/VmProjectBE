using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_VmProject.Migrations
{
    public partial class CookieUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cookie",
                schema: "VmProjectBE",
                columns: table => new
                {
                    cookie_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    session_token_id = table.Column<int>(type: "int", nullable: false),
                    cookie_name = table.Column<string>(type: "varchar(40)", nullable: false),
                    cookie_value = table.Column<string>(type: "varchar(300)", nullable: false),
                    site_from = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cookie", x => x.cookie_id);
                    table.ForeignKey(
                        name: "FK_cookie_session_token_session_token_id",
                        column: x => x.session_token_id,
                        principalSchema: "VmProjectBE",
                        principalTable: "session_token",
                        principalColumn: "session_token_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cookie_session_token_id",
                schema: "VmProjectBE",
                table: "cookie",
                column: "session_token_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cookie",
                schema: "VmProjectBE");
        }
    }
}
