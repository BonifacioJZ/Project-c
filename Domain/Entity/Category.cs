using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity
{
/* The Category class represents a category of products and includes properties for the category's
   ID, name, description, guard name, and a collection of products. */
    public class Category{
        public Guid Id { get; set; }
        [Column("name")]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = "";
        [Column("description")]
        [MaxLength(500)]
        public string Description { get; set; } = "";
        [Column("guard_name")]
        [Required]
        [MaxLength(13)]
        public string GuardName {get; set;} = "";
        public virtual ICollection<Product> Products {get; set;} = new List<Product>();
    }
}