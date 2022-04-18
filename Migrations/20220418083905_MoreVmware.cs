using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_VmProject.Migrations
{
    public partial class MoreVmware : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_access_token_user_user_id",
                schema: "DatabaseVmProject",
                table: "access_token");

            migrationBuilder.DropForeignKey(
                name: "FK_group_section_section_id",
                schema: "DatabaseVmProject",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_group_membership_group_group_id",
                schema: "DatabaseVmProject",
                table: "group_membership");

            migrationBuilder.DropForeignKey(
                name: "FK_group_membership_user_user_id",
                schema: "DatabaseVmProject",
                table: "group_membership");

            migrationBuilder.DropForeignKey(
                name: "FK_section_course_course_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropForeignKey(
                name: "FK_section_semester_semester_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropForeignKey(
                name: "FK_session_token_access_token_access_token_id",
                schema: "DatabaseVmProject",
                table: "session_token");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_tag_category_tag_category_id",
                schema: "DatabaseVmProject",
                table: "tag");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_user_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "tag_user");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_user_user_user_id",
                schema: "DatabaseVmProject",
                table: "tag_user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_section_role_role_role_id",
                schema: "DatabaseVmProject",
                table: "user_section_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_section_role_section_section_id",
                schema: "DatabaseVmProject",
                table: "user_section_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_section_role_user_user_id",
                schema: "DatabaseVmProject",
                table: "user_section_role");

            migrationBuilder.DropForeignKey(
                name: "FK_vlan_vswitch_vlan_vlan_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vlan_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_instance");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_ip_address_ip_address_ip_address_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_ip_address_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_tag_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_vswitch_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_template_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_template_tag_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vswitch_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vswitch_tag_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag");

            migrationBuilder.AddColumn<int>(
                name: "folder_id",
                schema: "DatabaseVmProject",
                table: "section",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "resource_group_id",
                schema: "DatabaseVmProject",
                table: "section",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "folder_id",
                schema: "DatabaseVmProject",
                table: "group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "course_code",
                schema: "DatabaseVmProject",
                table: "course",
                type: "varchar(45)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AddColumn<int>(
                name: "resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "folder",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    folder_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vcenter_folder_id = table.Column<string>(type: "varchar(45)", nullable: false),
                    folder_description = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_folder", x => x.folder_id);
                });

            migrationBuilder.CreateTable(
                name: "resource_group",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    resource_group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memory = table.Column<double>(type: "float", nullable: false),
                    cpu = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_group", x => x.resource_group_id);
                });

            migrationBuilder.CreateTable(
                name: "resource_group_template",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    resource_group_template_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memory = table.Column<double>(type: "float", nullable: false),
                    cpu = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_resource_group_template", x => x.resource_group_template_id);
                });

            migrationBuilder.CreateTable(
                name: "folder_user",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    folder_user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    folder_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_folder_user", x => x.folder_user_id);
                    table.ForeignKey(
                        name: "FK_folder_user_folder_folder_id",
                        column: x => x.folder_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "folder",
                        principalColumn: "folder_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_folder_user_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_section_folder_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "folder_id");

            migrationBuilder.CreateIndex(
                name: "IX_section_resource_group_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "resource_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_folder_id",
                schema: "DatabaseVmProject",
                table: "group",
                column: "folder_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course",
                column: "resource_group_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_folder_user_folder_id",
                schema: "DatabaseVmProject",
                table: "folder_user",
                column: "folder_id");

            migrationBuilder.CreateIndex(
                name: "IX_folder_user_UserId",
                schema: "DatabaseVmProject",
                table: "folder_user",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_access_token_user_user_id",
                schema: "DatabaseVmProject",
                table: "access_token",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_group_folder_folder_id",
                schema: "DatabaseVmProject",
                table: "group",
                column: "folder_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "folder",
                principalColumn: "folder_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_group_section_section_id",
                schema: "DatabaseVmProject",
                table: "group",
                column: "section_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "section",
                principalColumn: "section_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_group_membership_group_group_id",
                schema: "DatabaseVmProject",
                table: "group_membership",
                column: "group_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "group",
                principalColumn: "group_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_group_membership_user_user_id",
                schema: "DatabaseVmProject",
                table: "group_membership",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_section_course_course_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "course_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "course",
                principalColumn: "course_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_section_folder_folder_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "folder_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "folder",
                principalColumn: "folder_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_section_resource_group_resource_group_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "resource_group_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "resource_group",
                principalColumn: "resource_group_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_section_semester_semester_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "semester_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "semester",
                principalColumn: "semester_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_session_token_access_token_access_token_id",
                schema: "DatabaseVmProject",
                table: "session_token",
                column: "access_token_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "access_token",
                principalColumn: "access_token_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_tag_category_tag_category_id",
                schema: "DatabaseVmProject",
                table: "tag",
                column: "tag_category_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag_category",
                principalColumn: "tag_category_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_user_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "tag_user",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_user_user_user_id",
                schema: "DatabaseVmProject",
                table: "tag_user",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_section_role_role_role_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "role_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_section_role_section_section_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "section_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "section",
                principalColumn: "section_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_section_role_user_user_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vlan_vswitch_vlan_vlan_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch",
                column: "vlan_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vlan",
                principalColumn: "vlan_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vlan_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch",
                column: "vswitch_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vswitch",
                principalColumn: "vswitch_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_instance",
                column: "vm_template_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_template",
                principalColumn: "vm_template_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_ip_address_ip_address_ip_address_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address",
                column: "ip_address_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "ip_address",
                principalColumn: "ip_address_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_ip_address_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address",
                column: "vm_instance_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_instance",
                principalColumn: "vm_instance_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_tag_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag",
                column: "vm_instance_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_instance",
                principalColumn: "vm_instance_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_vswitch_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch",
                column: "vm_instance_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_instance",
                principalColumn: "vm_instance_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch",
                column: "vswitch_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vswitch",
                principalColumn: "vswitch_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_template_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_template_tag_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag",
                column: "vm_template_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_template",
                principalColumn: "vm_template_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vswitch_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vswitch_tag_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag",
                column: "vswitch_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vswitch",
                principalColumn: "vswitch_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_access_token_user_user_id",
                schema: "DatabaseVmProject",
                table: "access_token");

            migrationBuilder.DropForeignKey(
                name: "FK_course_resource_group_template_resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_group_folder_folder_id",
                schema: "DatabaseVmProject",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_group_section_section_id",
                schema: "DatabaseVmProject",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_group_membership_group_group_id",
                schema: "DatabaseVmProject",
                table: "group_membership");

            migrationBuilder.DropForeignKey(
                name: "FK_group_membership_user_user_id",
                schema: "DatabaseVmProject",
                table: "group_membership");

            migrationBuilder.DropForeignKey(
                name: "FK_section_course_course_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropForeignKey(
                name: "FK_section_folder_folder_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropForeignKey(
                name: "FK_section_resource_group_resource_group_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropForeignKey(
                name: "FK_section_semester_semester_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropForeignKey(
                name: "FK_session_token_access_token_access_token_id",
                schema: "DatabaseVmProject",
                table: "session_token");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_tag_category_tag_category_id",
                schema: "DatabaseVmProject",
                table: "tag");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_user_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "tag_user");

            migrationBuilder.DropForeignKey(
                name: "FK_tag_user_user_user_id",
                schema: "DatabaseVmProject",
                table: "tag_user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_section_role_role_role_id",
                schema: "DatabaseVmProject",
                table: "user_section_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_section_role_section_section_id",
                schema: "DatabaseVmProject",
                table: "user_section_role");

            migrationBuilder.DropForeignKey(
                name: "FK_user_section_role_user_user_id",
                schema: "DatabaseVmProject",
                table: "user_section_role");

            migrationBuilder.DropForeignKey(
                name: "FK_vlan_vswitch_vlan_vlan_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vlan_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_instance");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_ip_address_ip_address_ip_address_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_ip_address_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_tag_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_vswitch_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_instance_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_template_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vm_template_tag_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vswitch_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag");

            migrationBuilder.DropForeignKey(
                name: "FK_vswitch_tag_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag");

            migrationBuilder.DropTable(
                name: "folder_user",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "resource_group",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "resource_group_template",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "folder",
                schema: "DatabaseVmProject");

            migrationBuilder.DropIndex(
                name: "IX_section_folder_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropIndex(
                name: "IX_section_resource_group_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropIndex(
                name: "IX_group_folder_id",
                schema: "DatabaseVmProject",
                table: "group");

            migrationBuilder.DropIndex(
                name: "IX_course_resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course");

            migrationBuilder.DropColumn(
                name: "folder_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropColumn(
                name: "resource_group_id",
                schema: "DatabaseVmProject",
                table: "section");

            migrationBuilder.DropColumn(
                name: "folder_id",
                schema: "DatabaseVmProject",
                table: "group");

            migrationBuilder.DropColumn(
                name: "resource_group_template_id",
                schema: "DatabaseVmProject",
                table: "course");

            migrationBuilder.AlterColumn<string>(
                name: "course_code",
                schema: "DatabaseVmProject",
                table: "course",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)");

            migrationBuilder.AddForeignKey(
                name: "FK_access_token_user_user_id",
                schema: "DatabaseVmProject",
                table: "access_token",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_group_section_section_id",
                schema: "DatabaseVmProject",
                table: "group",
                column: "section_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "section",
                principalColumn: "section_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_group_membership_group_group_id",
                schema: "DatabaseVmProject",
                table: "group_membership",
                column: "group_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "group",
                principalColumn: "group_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_group_membership_user_user_id",
                schema: "DatabaseVmProject",
                table: "group_membership",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_section_course_course_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "course_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "course",
                principalColumn: "course_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_section_semester_semester_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "semester_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "semester",
                principalColumn: "semester_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_session_token_access_token_access_token_id",
                schema: "DatabaseVmProject",
                table: "session_token",
                column: "access_token_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "access_token",
                principalColumn: "access_token_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_tag_category_tag_category_id",
                schema: "DatabaseVmProject",
                table: "tag",
                column: "tag_category_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag_category",
                principalColumn: "tag_category_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_user_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "tag_user",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tag_user_user_user_id",
                schema: "DatabaseVmProject",
                table: "tag_user",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_section_role_role_role_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "role_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_section_role_section_section_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "section_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "section",
                principalColumn: "section_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_section_role_user_user_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "user_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vlan_vswitch_vlan_vlan_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch",
                column: "vlan_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vlan",
                principalColumn: "vlan_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vlan_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch",
                column: "vswitch_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vswitch",
                principalColumn: "vswitch_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_instance",
                column: "vm_template_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_template",
                principalColumn: "vm_template_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_ip_address_ip_address_ip_address_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address",
                column: "ip_address_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "ip_address",
                principalColumn: "ip_address_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_ip_address_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address",
                column: "vm_instance_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_instance",
                principalColumn: "vm_instance_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_tag_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag",
                column: "vm_instance_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_instance",
                principalColumn: "vm_instance_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_vswitch_vm_instance_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch",
                column: "vm_instance_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_instance",
                principalColumn: "vm_instance_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_instance_vswitch_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch",
                column: "vswitch_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vswitch",
                principalColumn: "vswitch_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_template_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vm_template_tag_vm_template_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag",
                column: "vm_template_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vm_template",
                principalColumn: "vm_template_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vswitch_tag_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag",
                column: "tag_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vswitch_tag_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag",
                column: "vswitch_id",
                principalSchema: "DatabaseVmProject",
                principalTable: "vswitch",
                principalColumn: "vswitch_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
