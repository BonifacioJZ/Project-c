using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity
{
    /* The Product class represents a product with properties such as id, name, description, price,
    quantity, is_enable, and category_id. */
    public class Product
    {
        public Guid id {get; set;}
        [Column("name")]
        [Required]
        [MaxLength(150)]
        public string Name {get; set;} = "";
        [Column("description")]
        [MaxLength(500)]
        public string Description {get; set;} = "";
        [Column("price")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        public decimal Price {get; set;}
        [Column("quantity")]
        public int Quantity {get; set;}
        [Column("is_enable")]
        public bool IsEnable {get;set;} = true;
        [Column("category_id")]
        public Guid CategoryId { get; set; }
        [Required]
        public virtual Category Category {get; set;} = new Category();
    }
}