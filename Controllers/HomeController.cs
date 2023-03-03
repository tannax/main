using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult UsuariosEdit()
        {
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult NovoUsuario(NovoUsuarioWithoutParams novoUsuario)
        {
            if (ModelState.IsValid)
            {
                // save the new user to the database
                return RedirectToAction("NovoUsuario");
            }

            return View("NovoUsuario");
        }
    }
}
