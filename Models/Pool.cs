using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("group", Schema = "VmProjectBE")]
    public class Pool
    {
        [Key]
        [Column("group_id", Order = 1)]
        public int PoolId { get; set; }

        [Required]
        [Column("canvas_group_id", Order = 2)]
        public int CanvasPoolId { get; set; }

        [Required]
        [Column("group_name", TypeName = "varchar(45)", Order = 3)]
        public string PoolName { get; set; }

        [Required]
        [Column("section_id", Order = 4)]
        public int SectionId { get; set; }

        [Required]
        [Column("folder_id", Order = 5)]
        public int FolderId { get; set; }


        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        [ForeignKey("FolderId")]
        public Folder Folder { get; set; }
    }
}