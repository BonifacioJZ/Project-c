using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Res;
using Domain.Dto.User;

namespace App.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<string>> Login(UserLogin userLogin);
        Task<Response<string>> Register(UserInDto register);
    }
}