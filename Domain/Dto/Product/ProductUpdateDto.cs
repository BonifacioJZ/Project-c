using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.Product
{
    public class ProductUpdateDto
    {
        [Required]
        public Guid id {get;set;}
        [MaxLength(150)]
        public string Name {get; set;} = "";
        [MaxLength(500)]
        public string Description {get; set;} = "";
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        public decimal Price {get; set;}
        public int Quantity {get; set;}
        public bool IsEnable {get;set;} = true;
        public Guid CategoryId { get; set; }
    }
}