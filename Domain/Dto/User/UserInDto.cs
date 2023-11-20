using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.User
{
    public class UserInDto 
    {
        [Required]
        public string FirstName {get;set;} = string.Empty;
        [Required]
        public string LastName {get;set;} = string.Empty;
        [Required]
        [MaxLength(32)]
        public string Username {get;set;} = string.Empty;
        [Required]
        [EmailAddress]
        public string Email {get; set;} = string.Empty;
        [Required]
        public string Password {get;set;} = string.Empty;
        [Required]
        public string ConfirmPassword {get;set;} = string.Empty;
    }
}