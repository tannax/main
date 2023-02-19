using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

/* Testando GIT*/
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
        public IActionResult Login(string login, string senha)
        {
            if(login != "admin" || senha != "123")
            {
                ViewData["Erro"] = "Senha inválida";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("user", "admin");
                return RedirectToAction("Index");
            }
        }

  [HttpPost]
        public IActionResult Login(Login u)
        {
            Login userFound = LoginBD.inserirLogin(u);
           if(u != null) {
            HttpContext.Session.SetInt32("id", userFound.id);
        HttpContext.Session.SetString("login", userFound.login);
        HttpContext.Session.SetString("senha", userFound.senha);
            return View ();
           }
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}
