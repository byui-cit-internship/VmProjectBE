﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VmProjectBE.Models
{
    [Table("ip_address", Schema = "VmProjectBE")]
    public class IpAddress
    {
        [Key]
        [Column("ip_address_id", Order = 1)]
        public int IpAddressId { get; set; }

        [Required]
        [Column("ip_address", TypeName = "binary(16)", Order = 2)]
        public byte[] IpAddressField { get; set; }

        [Required]
        [Column("subnet_mask", TypeName = "binary(16)", Order = 3)]
        public byte[] SubnetMask { get; set; }

        [Required]
        [Column("is_ipv6", TypeName = "bit", Order = 4)]
        public bool IsIpv6 { get; set; }
    }
}