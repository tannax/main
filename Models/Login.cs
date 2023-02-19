using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Models
{
    public class Login
    {
        public int id {get;set;}
        public string senha {get;set;}
        public string login {get;set;}   
    }
}