using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseVmProject.Models
{
    [Table("course", Schema = "DatabaseVmProject")]
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
        [Column("course_name", TypeName = "varchar(75)", Order = 3)]
        public string CourseName { get; set; }

        [Required]
        [Column("resource_group_id", Order = 4)]
        public int ResourceGroupId { get; set; }


        [ForeignKey("ResourceGroupId")]
        public ResourceGroup ResourceGroup { get; set; }
    }
}