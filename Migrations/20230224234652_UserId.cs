using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_VmProject.Migrations
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vswitch_tag_tag_tag_id",
                schema: "VmProjectBE",
                table: "vswitch_tag");

            migrationBuilder.DropIndex(
                name: "IX_vswitch_tag_tag_id",
                schema: "VmProjectBE",
                table: "vswitch_tag");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "VmProjectBE",
                table: "vm_instance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "VmProjectBE",
                table: "vm_instance");

            migrationBuilder.CreateIndex(
                name: "IX_vswitch_tag_tag_id",
                schema: "VmProjectBE",
                table: "vswitch_tag",
                column: "tag_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vswitch_tag_tag_tag_id",
                schema: "VmProjectBE",
                table: "vswitch_tag",
                column: "tag_id",
                principalSchema: "VmProjectBE",
                principalTable: "tag",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
