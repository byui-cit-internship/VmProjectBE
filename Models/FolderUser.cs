﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("folder_user", Schema = "VmProjectBE")]
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
        public int UserId { get; set; }


        [ForeignKey("FolderId")]
        public Folder Folder { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}