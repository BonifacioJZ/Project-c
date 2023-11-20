using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.Category
{
    public class CategoryInDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = "";
        [MaxLength(500)]
        public string Description { get; set; } = "";
        [Required]
        [MaxLength(13)]
        public string GuardName {get; set;} = "";
    }
}