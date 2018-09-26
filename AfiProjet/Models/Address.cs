using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiProjet.Models
{
[Table("Address", Schema = "SalesLT")]
    public partial class Address
    {
        public Address()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
            SalesOrderHeaderBillToAddresses = new HashSet<SalesOrderHeader>();
            SalesOrderHeaderShipToAddresses = new HashSet<SalesOrderHeader>();
        }

        [Column("AddressID")]
        public int AddressId { get; set; }
        [Required]
        [StringLength(60)]
        public string AddressLine1 { get; set; }
        [StringLength(60)]
        public string AddressLine2 { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        [StringLength(50)]
        public string StateProvince { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        [StringLength(50)]
        public string CountryRegion { get; set; }
        [Required]
        [StringLength(15)]
        public string PostalCode { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Address")]
        public ICollection<CustomerAddress> CustomerAddresses { get; set; }
        [InverseProperty("BillToAddress")]
        public ICollection<SalesOrderHeader> SalesOrderHeaderBillToAddresses { get; set; }
        [InverseProperty("ShipToAddress")]
        public ICollection<SalesOrderHeader> SalesOrderHeaderShipToAddresses { get; set; }
    }
}
