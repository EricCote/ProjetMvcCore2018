using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiProjet.Models
{
[Table("ProductModelProductDescription", Schema = "SalesLT")]
    public partial class ProductModelProductDescription
    {
        [Column("ProductModelID")]
        public int ProductModelId { get; set; }
        [Column("ProductDescriptionID")]
        public int ProductDescriptionId { get; set; }
        [StringLength(6)]
        public string Culture { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ProductDescriptionId")]
        [InverseProperty("ProductModelProductDescriptions")]
        public ProductDescription ProductDescription { get; set; }
        [ForeignKey("ProductModelId")]
        [InverseProperty("ProductModelProductDescriptions")]
        public ProductModel ProductModel { get; set; }
    }
}
