﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("session_token", Schema = "VmProjectBE")]
    public class SessionToken
    {
        [Key]
        [Column("session_token_id", Order = 1)]
        public int SessionTokenId { get; set; }

        [Required]
        [Column("sesion_token_value", TypeName = "uniqueidentifier", Order = 2)]
        public Guid SessionTokenValue { get; set; }

        [Required]
        [Column("expire_date", TypeName = "datetime2(7)", Order = 3)]
        public DateTime ExpireDate { get; set; }

        [Required]
        [Column("access_token_id", Order = 4)]
        public int AccessTokenId { get; set; }


        [ForeignKey("AccessTokenId")]
        public AccessToken AccessToken { get; set; }
    }
}