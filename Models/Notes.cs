using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("note", Schema = "VmProjectBE")]
    public class Note
    {
        // Primary Key
        [Key]
        [Column("note_id", Order = 1)]
        public int NotesId { get; set; }

        [Required]
        [Column("note", TypeName = "varchar(max)", Order = 2)]
        public string NoteDetail { get; set; }

        [ForeignKey("SectionId")]
        public VmInstance SectionId { get; set; }
    }
}