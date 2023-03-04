using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {


        public IActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovoUsuario(Usuario novoUser)
        {
            UsuarioService service = new UsuarioService();

            novoUser.senha = Criptografo.TextoCriptografado(novoUser.senha);

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);

            return RedirectToAction("cadastroRealizado");
        }
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdmin(this);
            return View(new UsuarioService().Listar());
        }

        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {
            UsuarioService us = new UsuarioService();
            us.editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdmin(this);
            return View(new NovoUsuarioWithoutParams());
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdmin(this);
            novoUser.senha = Criptografo.TextoCriptografado(novoUser.senha);

            using (BibliotecaContext bc = new BibliotecaContext()) {

            UsuarioService us = new UsuarioService();
            us.incluirUsuario(novoUser);
            return RedirectToAction("cadastroRealizado");
        }
        }




        public IActionResult ExcluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if (decisao == "EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão do usuario " + new UsuarioService().Listar(id).Nome + " realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
        }

        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioAdmin(this);
            return View();
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(ControllerContext);
            hash.Add(HttpContext);
            hash.Add(MetadataProvider);
            hash.Add(ModelBinderFactory);
            hash.Add(ModelState);
            hash.Add(ObjectValidator);
            hash.Add(ProblemDetailsFactory);
            hash.Add(Request);
            hash.Add(Response);
            hash.Add(RouteData);
            hash.Add(Url);
            hash.Add(User);
            hash.Add(TempData);
            hash.Add(ViewBag);
            hash.Add(ViewData);
            
            return hash.ToHashCode();
        }
    }
}

