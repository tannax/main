using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private const string V = "AdminOnly";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login u)
        {
            if (u == null)
            {
                return BadRequest("Login inválido");
            }

            Login userFound = LoginBD.inserirLogin(u);
            HttpContext.Session.SetInt32("id", userFound.id);
            HttpContext.Session.SetString("login", userFound.login);
            HttpContext.Session.SetString("senha", userFound.senha);
            HttpContext.Session.SetString("user", "admin");
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult usuariosEdit()
        {
            return View();
        }


        [Authorize(Policy = "AdminOnly")]
        public IActionResult NovoUsuario()
        {
            return RedirectToAction("NovoUsuarioWithoutParams");
        }

        public IActionResult NovoUsuarioWithoutParams()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }
        
    }
}