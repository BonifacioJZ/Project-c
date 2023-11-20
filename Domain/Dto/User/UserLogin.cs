using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.User
{
    public class UserLogin
    {
        [Required]
        [EmailAddress]
        private string Email {get;set;} = string.Empty;
        [Required]
        [EmailAddress]
        private string Password {get;set;} = string.Empty;
    }
}