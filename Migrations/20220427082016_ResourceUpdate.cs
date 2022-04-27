using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_VmProject.Migrations
{
    public partial class ResourceUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_resource_group_template_resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_folder_user_user_UserId",
                schema: "DatabaseVmProject",
                table: "folder_user");

            migrationBuilder.DropTable(
                name: "resource_group_template",
                schema: "DatabaseVmProject");

            migrationBuilder.DropIndex(
                name: "IX_folder_user_UserId",
                schema: "DatabaseVmProject",
                table: "folder_user");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "DatabaseVmProject",
                table: "folder_user");

            migrationBuilder.RenameColumn(
                name: "resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course",
                newName: "resource_group_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course",
                newName: "IX_course_resource_group_id");

            migrationBuilder.AddColumn<string>(
                name: "resource_group_name",
                schema: "DatabaseVmProject",
                table: "resource_group",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "resource_group_vsphere_id",
                schema: "DatabaseVmProject",
                table: "resource_group",
                type: "varchar(15)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_folder_user_user_id",
                schema: "DatabaseVmProject",
                table: "folder_user",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_course_resource_group_resource_group_id",
                schema: "DatabaseVmProject",
                table: "course",
                column: "resource_group_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "resource_group",
                principalColumn: "resource_group_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_folder_user_user_user_id",
                schema: "DatabaseVmProject",
                table: "folder_user",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_resource_group_resource_group_id",
                schema: "DatabaseVmProject",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_folder_user_user_user_id",
                schema: "DatabaseVmProject",
                table: "folder_user");

            migrationBuilder.DropIndex(
                name: "IX_folder_user_user_id",
                schema: "DatabaseVmProject",
                table: "folder_user");

            migrationBuilder.DropColumn(
                name: "resource_group_name",
                schema: "DatabaseVmProject",
                table: "resource_group");

            migrationBuilder.DropColumn(
                name: "resource_group_vsphere_id",
                schema: "DatabaseVmProject",
                table: "resource_group");

            migrationBuilder.RenameColumn(
                name: "resource_group_id",
                schema: "DatabaseVmProject",
                table: "course",
                newName: "resource_group_template_id");

            migrationBuilder.RenameIndex(
                name: "IX_course_resource_group_id",
                schema: "DatabaseVmProject",
                table: "course",
                newName: "IX_course_resource_group_template_id");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "DatabaseVmProject",
                table: "folder_user",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "resource_group_template",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    resource_group_template_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cpu = table.Column<double>(type: "float", nullable: false),
                    memory = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_group_template", x => x.resource_group_template_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_folder_user_UserId",
                schema: "DatabaseVmProject",
                table: "folder_user",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_course_resource_group_template_resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course",
                column: "resource_group_template_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "resource_group_template",
                principalColumn: "resource_group_template_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_folder_user_user_UserId",
                schema: "DatabaseVmProject",
                table: "folder_user",
                column: "UserId",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
