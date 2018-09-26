using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfiProjet.Models
{
[Table("Product", Schema = "SalesLT")]
    public partial class Product
    {
        public Product()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        [Column("ProductID")]
        public int ProductId { get; set; }
        [Required]
        [Column(TypeName = "Name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; }
        [StringLength(15)]
        [DataType("Color")]
        public string Color { get; set; }
        [Column(TypeName = "money")]
        public decimal StandardCost { get; set; }
        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal ListPrice { get; set; }
        [StringLength(5)]
        public string Size { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? Weight { get; set; }
        [Column("ProductCategoryID")]
        public int? ProductCategoryId { get; set; }
        [Column("ProductModelID")]
        public int? ProductModelId { get; set; }
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        [UIHint("DateRouge")]
        public DateTime SellStartDate { get; set; }
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? SellEndDate { get; set; }
        [Column(TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime? DiscontinuedDate { get; set; }
        //public byte[] ThumbNailPhoto { get; set; }
        [StringLength(50)]
        public string ThumbnailPhotoFileName { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ProductCategoryId")]
        [InverseProperty("Products")]
        public ProductCategory ProductCategory { get; set; }
        [ForeignKey("ProductModelId")]
        [InverseProperty("Products")]
        public ProductModel ProductModel { get; set; }
        [InverseProperty("Product")]
        public ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        public ProductImage ProductImage { get; set; }
    }


    [Table("Product", Schema = "SalesLT")]
    public class ProductImage {
        [Column("ProductID")]
        [Key()]
        public int ProductId { get; set; }
        public byte[] ThumbNailPhoto { get; set; }
        public Product Product { get; set; }
    }

}
