using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VmProjectBE.Models
{
    [Table("semester", Schema = "VmProjectBE")]
    public class Semester
    {
        [Key]
        [Column("semester_id", Order = 1)]
        public int SemesterId { get; set; }

        [Required]
        [Column("enrollment_term_canvas_id", Order = 2)]
        public int EnrollmentTermCanvasId { get; set; }

        [Required]
        [Column("semester_year", Order = 3)]
        public int SemesterYear { get; set; }

        [Required]
        [Column("semester_term", TypeName = "varchar(20)", Order = 4)]
        public string SemesterTerm { get; set; }

        [Required]
        [Column("start_date", TypeName = "datetime2(7)", Order = 5)]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("end_date", TypeName = "datetime2(7)", Order = 6)]
        public DateTime EndDate { get; set; }
    }
}