using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseVmProject.Models
{
    [Table("resource_group", Schema = "DatabaseVmProject")]
    public class ResourceGroup
    {
        // Primary Key
        [Key]
        [Column("resource_group_id", Order = 1)]
        public int ResourceGroupId { get; set; }

        [Required]
        [Column("resource_group_name", TypeName = "varchar(20)", Order = 2)]
        public string ResourceGroupName { get; set; }

        [Column("resource_group_vsphere_id", TypeName = "varchar(15)", Order = 3)]
        public string ResourceGroupVsphereId { get; set; }

        [Required]
        [Column("memory", TypeName = "float", Order = 4)]
        public double Memory { get; set; }

        [Required]
        [Column("cpu", TypeName = "float", Order = 5)]
        public double Cpu { get; set; }
    }
}