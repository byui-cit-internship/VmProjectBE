using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("section", Schema = "VmProjectBE")]
    public class Section
    {
        [Key]
        [Column("section_id", Order = 1)]
        public int SectionId { get; set; }

        [Required]
        [Column("course_id", Order = 2)]
        public int CourseId { get; set; }

        [Required]
        [Column("semester_id", Order = 3)]
        public int SemesterId { get; set; }

        [Required]
        [Column("folder_id", Order = 4)]
        public int FolderId { get; set; }

        [Required]
        [Column("resource_group_id", Order = 5)]
        public int ResourceGroupId { get; set; }

        [Required]
        [Column("section_number", Order = 6)]
        public int SectionNumber { get; set; }

        [Required]
        [Column("section_canvas_id", Order = 7)]
        public int SectionCanvasId { get; set; }

        [Required]
        [Column("section_name", Order = 8)]
        public string SectionName { get; set; }

        [Required]
        [Column("library_id", Order = 9)]
        public string LibraryId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }

        [ForeignKey("FolderId")]
        public Folder Folder { get; set; }

        [ForeignKey("ResourceGroupId")]
        public ResourceGroup ResourceGroup { get; set; }
    }
}