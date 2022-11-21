using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("group_membership", Schema = "VmProjectBE")]
    public class PoolMembership
    {
        [Key]
        [Column("group_membership_id", Order = 1)]
        public int PoolMembershipId { get; set; }

        [Required]
        [Column("group_id", Order = 2)]
        public int PoolId { get; set; }

        [Required]
        [Column("user_id", Order = 3)]
        public int UserId { get; set; }


        [ForeignKey("GroupId")]
        public Pool Pool { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}