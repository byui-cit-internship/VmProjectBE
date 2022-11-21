using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("resource_group", Schema = "VmProjectBE")]
    public class ResourcePool
    {
        // Primary Key
        [Key]
        [Column("resource_group_id", Order = 1)]
        public int ResourcePoolId { get; set; }

        [Required]
        [Column("resource_group_name", TypeName = "varchar(20)", Order = 2)]
        public string ResourcePoolName { get; set; }

        [Column("resource_group_vsphere_id", TypeName = "varchar(15)", Order = 3)]
        public string ResourcePoolVsphereId { get; set; }

        [Required]
        [Column("memory", TypeName = "float", Order = 4)]
        public double Memory { get; set; }

        [Required]
        [Column("cpu", TypeName = "float", Order = 5)]
        public double Cpu { get; set; }
    }
}