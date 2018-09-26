using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiProjet.Models
{
[Table("Customer", Schema = "SalesLT")]
    public partial class Customer
    {
        public Customer()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
            SalesOrderHeaders = new HashSet<SalesOrderHeader>();
        }

        [Column("CustomerID")]
        public int CustomerId { get; set; }
        [Column(TypeName = "NameStyle")]
        public bool NameStyle { get; set; }
        [StringLength(8)]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column(TypeName = "Name")]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(10)]
        public string Suffix { get; set; }
        [StringLength(128)]
        public string CompanyName { get; set; }
        [StringLength(256)]
        public string SalesPerson { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [Column(TypeName = "Phone")]
        [StringLength(25)]
        public string Phone { get; set; }
        [Required]
        [StringLength(128)]
        public string PasswordHash { get; set; }
        [Required]
        [StringLength(10)]
        public string PasswordSalt { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Customer")]
        public ICollection<CustomerAddress> CustomerAddresses { get; set; }
        [InverseProperty("Customer")]
        public ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}
