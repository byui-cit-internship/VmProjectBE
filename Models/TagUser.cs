﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseVmProject.Models
{
    [Table("tag_user", Schema = "DatabaseVmProject")]
    public class TagUser
    {
        [Key]
        [Column("tag_user_id", Order = 1)]
        public int TagUserId { get; set; }

        [Required]
        [Column("tag_id", Order = 2)]
        public int TagId { get; set; }

        [Required]
        [Column("user_id", Order = 3)]
        public int UserId { get; set; }


        [ForeignKey("TagId")]
        public Tag Tag { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}