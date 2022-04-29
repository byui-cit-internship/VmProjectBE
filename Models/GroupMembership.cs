using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("group_membership", Schema = "VmProjectBE")]
    public class GroupMembership
    {
        [Key]
        [Column("group_membership_id", Order = 1)]
        public int GroupMembershipId { get; set; }

        [Required]
        [Column("group_id", Order = 2)]
        public int GroupId { get; set; }

        [Required]
        [Column("user_id", Order = 3)]
        public int UserId { get; set; }


        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}