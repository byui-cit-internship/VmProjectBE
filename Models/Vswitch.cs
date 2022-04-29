using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("vswitch", Schema = "VmProjectBE")]
    public class Vswitch
    {
        [Key]
        [Column("vswitch_id", Order = 1)]
        public int VswitchId { get; set; }

        [Required]
        [Column("vswitch_name", TypeName = "varchar(45)", Order = 2)]
        public string VswitchName { get; set; }

        [Required]
        [Column("vswitch_description", TypeName = "varchar(45)", Order = 3)]
        public string VswitchDescription { get; set; }
    }
}