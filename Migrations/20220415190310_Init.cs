using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database_VmProject.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DatabaseVmProject");

            migrationBuilder.CreateTable(
                name: "course",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_code = table.Column<string>(type: "varchar(15)", nullable: false),
                    course_name = table.Column<string>(type: "varchar(75)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "ip_address",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    ip_address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ip_address = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    subnet_mask = table.Column<byte[]>(type: "binary(16)", nullable: false),
                    is_ipv6 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ip_address", x => x.ip_address_id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "varchar(20)", nullable: false),
                    canvas_role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "semester",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    semester_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    semester_year = table.Column<int>(type: "int", nullable: false),
                    semester_term = table.Column<string>(type: "varchar(20)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_semester", x => x.semester_id);
                });

            migrationBuilder.CreateTable(
                name: "tag_category",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    tag_category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_category_vcenter_id = table.Column<string>(type: "varchar(100)", nullable: false),
                    tag_category_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    tag_category_description = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_category", x => x.tag_category_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(20)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(20)", nullable: false),
                    email = table.Column<string>(type: "varchar(30)", nullable: false),
                    is_admin = table.Column<bool>(type: "bit", nullable: false),
                    canvas_token = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "vlan",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vlan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vlan_number = table.Column<int>(type: "int", nullable: false),
                    vlan_description = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vlan", x => x.vlan_id);
                });

            migrationBuilder.CreateTable(
                name: "vm_template",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vm_template_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vm_template_vcenter_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    vm_template_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    vm_template_access_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_template", x => x.vm_template_id);
                });

            migrationBuilder.CreateTable(
                name: "vswitch",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vswitch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vswitch_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    vswitch_description = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vswitch", x => x.vswitch_id);
                });

            migrationBuilder.CreateTable(
                name: "section",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    section_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    semester_id = table.Column<int>(type: "int", nullable: false),
                    section_number = table.Column<int>(type: "int", nullable: false),
                    section_canvas_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_section", x => x.section_id);
                    table.ForeignKey(
                        name: "FK_section_course_course_id",
                        column: x => x.course_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "course",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_section_semester_semester_id",
                        column: x => x.semester_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "semester",
                        principalColumn: "semester_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_category_id = table.Column<int>(type: "int", nullable: false),
                    tag_vcenter_id = table.Column<string>(type: "varchar(100)", nullable: false),
                    tag_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    tag_description = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.tag_id);
                    table.ForeignKey(
                        name: "FK_tag_tag_category_tag_category_id",
                        column: x => x.tag_category_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "tag_category",
                        principalColumn: "tag_category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "access_token",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    access_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    access_token_value = table.Column<string>(type: "varchar(200)", nullable: false),
                    expire_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_token", x => x.access_token_id);
                    table.ForeignKey(
                        name: "FK_access_token_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vm_instance",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vm_instance_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vm_template_id = table.Column<int>(type: "int", nullable: false),
                    vm_instance_vcenter_id = table.Column<string>(type: "varchar(50)", nullable: false),
                    vm_instance_expire_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_instance", x => x.vm_instance_id);
                    table.ForeignKey(
                        name: "FK_vm_instance_vm_template_vm_template_id",
                        column: x => x.vm_template_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vm_template",
                        principalColumn: "vm_template_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vlan_vswitch",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vlan_vswitch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vlan_id = table.Column<int>(type: "int", nullable: false),
                    vswitch_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vlan_vswitch", x => x.vlan_vswitch_id);
                    table.ForeignKey(
                        name: "FK_vlan_vswitch_vlan_vlan_id",
                        column: x => x.vlan_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vlan",
                        principalColumn: "vlan_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vlan_vswitch_vswitch_vswitch_id",
                        column: x => x.vswitch_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vswitch",
                        principalColumn: "vswitch_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    canvas_group_id = table.Column<int>(type: "int", nullable: false),
                    group_name = table.Column<string>(type: "varchar(45)", nullable: false),
                    section_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.group_id);
                    table.ForeignKey(
                        name: "FK_group_section_section_id",
                        column: x => x.section_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "section",
                        principalColumn: "section_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_section_role",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    user_section_role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    section_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_section_role", x => x.user_section_role_id);
                    table.ForeignKey(
                        name: "FK_user_section_role_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_section_role_section_section_id",
                        column: x => x.section_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "section",
                        principalColumn: "section_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_section_role_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag_user",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    tag_user_id = table.Column<int>(name: "tag)_user_id", type: "int", nullable: false)
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
                        principalSchema: "DatabaseVmProject",
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag_user_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vm_template_tag",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vm_template_tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_id = table.Column<int>(type: "int", nullable: false),
                    vm_template_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_template_tag", x => x.vm_template_tag_id);
                    table.ForeignKey(
                        name: "FK_vm_template_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vm_template_tag_vm_template_vm_template_id",
                        column: x => x.vm_template_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vm_template",
                        principalColumn: "vm_template_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vswitch_tag",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vswitch_tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_id = table.Column<int>(type: "int", nullable: false),
                    vswitch_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vswitch_tag", x => x.vswitch_tag_id);
                    table.ForeignKey(
                        name: "FK_vswitch_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vswitch_tag_vswitch_vswitch_id",
                        column: x => x.vswitch_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vswitch",
                        principalColumn: "vswitch_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session_token",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    session_token_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sesion_token_value = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    expire_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    access_token_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_token", x => x.session_token_id);
                    table.ForeignKey(
                        name: "FK_session_token_access_token_access_token_id",
                        column: x => x.access_token_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "access_token",
                        principalColumn: "access_token_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vm_instance_ip_address",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vm_instance_ip_address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vm_instance_id = table.Column<int>(type: "int", nullable: false),
                    ip_address_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_instance_ip_address", x => x.vm_instance_ip_address_id);
                    table.ForeignKey(
                        name: "FK_vm_instance_ip_address_ip_address_ip_address_id",
                        column: x => x.ip_address_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "ip_address",
                        principalColumn: "ip_address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vm_instance_ip_address_vm_instance_vm_instance_id",
                        column: x => x.vm_instance_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vm_instance",
                        principalColumn: "vm_instance_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vm_instance_tag",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vm_instance_tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_id = table.Column<int>(type: "int", nullable: false),
                    vm_instance_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_instance_tag", x => x.vm_instance_tag_id);
                    table.ForeignKey(
                        name: "FK_vm_instance_tag_tag_tag_id",
                        column: x => x.tag_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vm_instance_tag_vm_instance_vm_instance_id",
                        column: x => x.vm_instance_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vm_instance",
                        principalColumn: "vm_instance_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vm_instance_vswitch",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    vm_instance_vswitch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vm_instance_id = table.Column<int>(type: "int", nullable: false),
                    vswitch_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vm_instance_vswitch", x => x.vm_instance_vswitch_id);
                    table.ForeignKey(
                        name: "FK_vm_instance_vswitch_vm_instance_vm_instance_id",
                        column: x => x.vm_instance_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vm_instance",
                        principalColumn: "vm_instance_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vm_instance_vswitch_vswitch_vswitch_id",
                        column: x => x.vswitch_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "vswitch",
                        principalColumn: "vswitch_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_membership",
                schema: "DatabaseVmProject",
                columns: table => new
                {
                    group_membership_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_membership", x => x.group_membership_id);
                    table.ForeignKey(
                        name: "FK_group_membership_group_group_id",
                        column: x => x.group_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_membership_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "DatabaseVmProject",
                        principalTable: "user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_token_user_id",
                schema: "DatabaseVmProject",
                table: "access_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_section_id",
                schema: "DatabaseVmProject",
                table: "group",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_membership_group_id",
                schema: "DatabaseVmProject",
                table: "group_membership",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_membership_user_id",
                schema: "DatabaseVmProject",
                table: "group_membership",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_section_course_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_section_semester_id",
                schema: "DatabaseVmProject",
                table: "section",
                column: "semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_token_access_token_id",
                schema: "DatabaseVmProject",
                table: "session_token",
                column: "access_token_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_tag_category_id",
                schema: "DatabaseVmProject",
                table: "tag",
                column: "tag_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_user_tag_id",
                schema: "DatabaseVmProject",
                table: "tag_user",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_user_user_id",
                schema: "DatabaseVmProject",
                table: "tag_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_section_role_role_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_section_role_section_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_section_role_user_id",
                schema: "DatabaseVmProject",
                table: "user_section_role",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_vlan_vswitch_vlan_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch",
                column: "vlan_id");

            migrationBuilder.CreateIndex(
                name: "IX_vlan_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vlan_vswitch",
                column: "vswitch_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_instance",
                column: "vm_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_ip_address_ip_address_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address",
                column: "ip_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_ip_address_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_ip_address",
                column: "vm_instance_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_tag_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_tag",
                column: "vm_instance_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_vswitch_vm_instance_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch",
                column: "vm_instance_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_instance_vswitch_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vm_instance_vswitch",
                column: "vswitch_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_template_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_vm_template_tag_vm_template_id",
                schema: "DatabaseVmProject",
                table: "vm_template_tag",
                column: "vm_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_vswitch_tag_tag_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_vswitch_tag_vswitch_id",
                schema: "DatabaseVmProject",
                table: "vswitch_tag",
                column: "vswitch_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "group_membership",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "session_token",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "tag_user",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "user_section_role",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vlan_vswitch",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vm_instance_ip_address",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vm_instance_tag",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vm_instance_vswitch",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vm_template_tag",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vswitch_tag",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "group",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "access_token",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "role",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vlan",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "ip_address",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vm_instance",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "tag",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vswitch",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "section",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "user",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "vm_template",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "tag_category",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "course",
                schema: "DatabaseVmProject");

            migrationBuilder.DropTable(
                name: "semester",
                schema: "DatabaseVmProject");
        }
    }
}
