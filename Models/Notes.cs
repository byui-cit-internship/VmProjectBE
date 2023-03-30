using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("notes", Schema = "VmProjectBE")]
    public class Notes
    {
        // Primary Key
        [Key]
        [Column("note_id", Order = 1)]
        public int NotesId { get; set; }

        [Required]
        [Column("note", TypeName = "varchar(max)", Order = 2)]
        public string Note { get; set; }

        [ForeignKey("SectionId")]
        public Section SectionId { get; set; }
    }
}