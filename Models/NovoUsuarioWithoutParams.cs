using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Biblioteca.Models;

namespace Biblioteca.Models
{
    public class NovoUsuarioWithoutParams
    {
        internal string login = "";
        internal string senha = "";
        internal int tipo = 0;
    }
}
