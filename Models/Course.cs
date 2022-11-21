using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VmProjectBE.Models
{
    [Table("course", Schema = "VmProjectBE")]
    public class Course
    {
        // Primary Key
        [Key]
        [Column("course_id", Order = 1)]
        public int CourseId { get; set; }

        [Required]
        [Column("course_code", TypeName = "varchar(45)", Order = 2)]
        public string CourseCode { get; set; }

        [Required]
        [Column("resource_group_id", Order = 3)]
        public int ResourcePoolId { get; set; }


        [ForeignKey("ResourcePoolId")]
        public ResourcePool ResourcePool { get; set; }
    }
}