using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class templateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tag_user",
                schema: "VmProjectBE");

            // migrationBuilder.AlterColumn<string>(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_template_tag",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(int),
            //     oldType: "int");

            // migrationBuilder.AlterColumn<string>(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_template",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(int),
            //     oldType: "int")
            //     .OldAnnotation("SqlServer:Identity", "1, 1");

            // migrationBuilder.AlterColumn<string>(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_instance",
            //     type: "nvarchar(450)",
            //     nullable: false,
            //     oldClrType: typeof(int),
            //     oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<int>(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_template_tag",
            //     type: "int",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");

            // migrationBuilder.AlterColumn<int>(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_template",
            //     type: "int",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)")
            //     .Annotation("SqlServer:Identity", "1, 1");

            // migrationBuilder.AlterColumn<int>(
            //     name: "vm_template_id",
            //     schema: "VmProjectBE",
            //     table: "vm_instance",
            //     type: "int",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "tag_user",
                schema: "VmProjectBE",
                columns: table => new
                {
                    tag_user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_user", x => x.tag_user_id);
                    table.ForeignKey(
                        name: "FK_tag_user_tag_tag_id",
                        column: x => x.tag_id,
                        principalSchema: "VmProjectBE",
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tag_user_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "VmProjectBE",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tag_user_tag_id",
                schema: "VmProjectBE",
                table: "tag_user",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_user_user_id",
                schema: "VmProjectBE",
                table: "tag_user",
                column: "user_id");
        }
    }
}
