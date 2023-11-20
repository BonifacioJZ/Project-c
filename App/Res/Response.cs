using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Res
{
    public class Response<T>
    {
        public bool Success { get; set; } = false;
        public string message {get; set; } = "error";
        public T? Data {get; set;} 
    }
}