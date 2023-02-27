using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("vswitch_tag", Schema = "VmProjectBE")]
    public class VswitchTag
    {
        [Key]
        [Column("vswitch_tag_id", Order = 1)]
        public int VswitchTagId { get; set; }

        [Required]
        [Column("tag_id", Order = 2)]
        public int TagId { get; set; }

        [Required]
        [Column("vswitch_id", Order = 3)]
        public int VswitchId { get; set; }

        [ForeignKey("VswitchId")]
        public Vswitch Vswitch { get; set; }
    }
}