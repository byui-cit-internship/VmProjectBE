using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DatabaseVmProject.Models
{
    [Table("cookie", Schema = "DatabaseVmProject")]
    public class Cookie
    {
        // Primary Key
        [Key]
        [Column("cookie_id", Order = 1)]
        public int CookieId { get; set; }


        [Column("session_token_id", Order = 2)]
        public int SessionTokenId { get; set; }

        [Required]
        [Column("cookie_name", TypeName = "varchar(40)", Order = 3)]
        public string CookieName { get; set; }

        [Required]
        [Column("cookie_value", TypeName = "varchar(100)", Order = 4)]
        public string CookieValue { get; set; }

        [Required]
        [Column("site_from", Order = 5)]
        public string SiteFrom { get; set; }


        [ForeignKey("SessionTokenId")]
        public SessionToken SessionToken { get; set; }
    }
}