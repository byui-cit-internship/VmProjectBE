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
        public string VmTemplateId { get; set; }

        // [Required]
        // [Column("vm_template_id2", Order = 2)]
        // public string VmTemplateId2 { get; set; }

        [Required]
        [Column("vm_instance_vcenter_name", Order = 3)]
        public string VmInstanceVcenterName {get; set;}

        [Required]
        [Column("vm_instance_vcenter_id", TypeName = "varchar(50)", Order = 4)]
        public string VmInstanceVcenterId { get; set; }

        [Required]
        [Column("vm_instance_create_date", TypeName = "datetime2(7)", Order = 5)]
        public DateTime VmInstanceCreationDate { get; set; }

        [Required]
        [Column("vm_instance_expire_date", TypeName = "datetime2(7)", Order = 6)]
        public DateTime VmInstanceExpireDate { get; set; }

        [Required]
        [Column("SectionId")]
        public int SectionId { get; set; }


        /*Added a new column so TagUser could eventually be deleted. Required was was not added incase the code freaks out*/
        [Column("UserId")]
        public int UserId{get; set;}
    }
}