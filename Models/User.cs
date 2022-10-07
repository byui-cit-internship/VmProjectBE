using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("user", Schema = "VmProjectBE")]
    public class User
    {
        [Key]
        [Column("user_id", Order = 1)]
        public int UserId { get; set; }

        [Required]
        [Column("first_name", TypeName = "varchar(20)", Order = 2)]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name", TypeName = "varchar(20)", Order = 3)]
        public string LastName { get; set; }

        [Required]
        [Column("email", TypeName = "varchar(30)", Order = 4)]
        public string Email { get; set; }

        [Required]
        [Column("is_admin", TypeName = "bit", Order = 5)]
        public bool IsAdmin { get; set; }

        [Column("canvas_token", TypeName = "varchar(100)", Order = 6)]
        public string CanvasToken { get; set; }


        [Column("encrypted_canvas_token", TypeName = "varbinary(MAX)", Order = 7)]
        public Byte[] EncryptedCanvasToken { get; set; }
    }
}