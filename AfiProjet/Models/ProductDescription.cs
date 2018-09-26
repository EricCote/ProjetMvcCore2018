using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiProjet.Models
{
[Table("ProductDescription", Schema = "SalesLT")]
    public partial class ProductDescription
    {
        public ProductDescription()
        {
            ProductModelProductDescriptions = new HashSet<ProductModelProductDescription>();
        }

        [Column("ProductDescriptionID")]
        public int ProductDescriptionId { get; set; }
        [Required]
        [StringLength(400)]
        public string Description { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("ProductDescription")]
        public ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }
    }
}
