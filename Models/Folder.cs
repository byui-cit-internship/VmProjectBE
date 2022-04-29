using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("folder", Schema = "VmProjectBE")]
    public class Folder
    {
        // Primary Key
        [Key]
        [Column("folder_id", Order = 1)]
        public int FolderId { get; set; }

        [Required]
        [Column("vcenter_folder_id", TypeName = "varchar(45)", Order = 2)]
        public string VcenterFolderId { get; set; }

        [Column("folder_description", TypeName = "varchar(100)", Order = 3)]
        public string FolderDescription { get; set; }
    }
}