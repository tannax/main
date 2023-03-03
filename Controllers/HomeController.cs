using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UsuariosEdit()
        {
            return View();
        }

        public IActionResult NovoUsuario() => View();

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult NovoUsuario(NovoUsuarioWithoutParams novoUsuario)
        {
            // TODO: Add code to create a new user with the provided parameters
            return RedirectToAction("Index");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add session middleware
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Configure authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("user", "admin"));
            });

            // Other service configurations...
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

            // Set user role or claim
            if (userFound.tipo == 1)
            {
                HttpContext.Session.SetString("user", "admin");
            }
            else
            {
                HttpContext.Session.SetString("user", "normal");
            }

            return RedirectToAction("Index");

        }


    }
}
