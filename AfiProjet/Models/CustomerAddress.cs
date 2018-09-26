using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiProjet.Models
{
[Table("CustomerAddress", Schema = "SalesLT")]
    public partial class CustomerAddress
    {
        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column("AddressID")]
        public int AddressId { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        [StringLength(50)]
        public string AddressType { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("AddressId")]
        [InverseProperty("CustomerAddresses")]
        public Address Address { get; set; }
        [ForeignKey("CustomerId")]
        [InverseProperty("CustomerAddresses")]
        public Customer Customer { get; set; }
    }
}
