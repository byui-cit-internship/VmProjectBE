using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseVmProject.Models
{
    [Table("folder_user", Schema = "DatabaseVmProject")]
    public class FolderUser
    {
        // Primary Key
        [Key]
        [Column("folder_user_id", Order = 1)]
        public int FolderUserId { get; set; }

        [Required]
        [Column("folder_id", Order = 2)]
        public int FolderId { get; set; }

        [Column("user_id", Order = 3)]
        public int user_id { get; set; }


        [ForeignKey("FolderId")]
        public Folder Folder { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}