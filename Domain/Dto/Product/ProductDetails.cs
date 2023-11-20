using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Category;

namespace Domain.Dto.Product
{
    public class ProductDetails
    {
        public Guid id {get; set;}
        public string Name {get; set;} = "";
        public string Description {get; set;} = "";
        public decimal Price {get; set;}
        public int Quantity {get; set;}
        public bool IsEnable {get;set;} 
        public virtual CategoryOutDto Category {get; set;}
    }
}