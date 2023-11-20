using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Product;

namespace Domain.Dto.Category
{
    public class CategoryDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string GuardName {get; set;} = "";
        public ICollection<ProductOutDto> products {get; set;} 
    }
}