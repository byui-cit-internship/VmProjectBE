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
        [Column("memory", TypeName = "float", Order = 2)]
        public double Memory { get; set; }

        [Column("cpu", TypeName = "float", Order = 3)]
        public double Cpu { get; set; }
    }
}