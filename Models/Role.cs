using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("role", Schema = "VmProjectBE")]
    public class Role
    {
        [Key]
        [Column("role_id", Order = 1)]
        public int RoleId { get; set; }

        [Required]
        [Column("role_name", TypeName = "varchar(20)", Order = 2)]
        public string RoleName { get; set; }

        [Column("canvas_role_id", Order = 3)]
        public int? CanvasRoleId { get; set; }
    }
}