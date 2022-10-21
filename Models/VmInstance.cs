using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("vm_instance", Schema = "VmProjectBE")]
    public class VmInstance
    {
        [Key]
        [Column("vm_instance_id", Order = 1)]
        public int VmInstanceId { get; set; }

        [Required]
        [Column("vm_template_id", Order = 2)]
        public int VmTemplateId { get; set; }


        [Column("vm_instance_vcenter_name", Order = 3)]
        public string VmInstanceVcenterName {get; set;}

        [Required]
        [Column("vm_instance_vcenter_id", TypeName = "varchar(50)", Order = 4)]
        public string VmInstanceVcenterId { get; set; }

        [Required]
        [Column("vm_instance_expire_date", TypeName = "datetime2(7)", Order = 5)]
        public DateTime VmInstanceExpireDate { get; set; }


        [ForeignKey("VmTemplateId")]
        public VmTemplate VmTemplate { get; set; }
    }
}