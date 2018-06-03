using Shop.Model.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Shop.Model.Model
{
    [Table("Products")]
    public class Product : Auditable
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public String Alias { get; set; }
        public string CategoryID { get; set; }
        public string Image { get; set; }
        public XElement MoreImages { get; set; }
        public decimal Price { get; set; }
        public decimal? PromitionPrice { get; set; }
        public int? Warranty { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }
        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }

    }
}
