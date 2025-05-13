using Dominio;
using IUWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IUWebApp.Controllers
{
    public class HomeController : Controller
    {
        Sistema s = Sistema.Instancia();
        public IActionResult Index()
        {
            int? idUsuarioLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
            string? rolUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            Usuario u = s.GetUsuarioLogueado(idUsuarioLogueado); //igualamos el id de la session con el de nuestra lista de usuarios, si coinciden retornamos ese usuario.

            if (u != null)
            {
                if (rolUsuarioLogueado == "Administrador")
                {
                    ViewBag.msg = "Administrador"; //a la vista
                }
                else
                {
                    Miembro m = (Miembro)u;
                    ViewBag.msg = "Miembro " + m.Nombre;
                    if (m.Bloqueado)
                    {
                        ViewBag.msgBloqueado = "Le informamos que usted ha sido bloqueado por un administrador de la red, por lo cual tendrá acciones restringidas";
                    }
                }
            }
            else
            {
                ViewBag.msg = "Inicie Sesión para acceder a las funcionalidades de nuestra red, si aún no está registrado puede Registrarse de froma gratuita";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion(string Email, string Contrasenia)
        {
            try
            {
                Usuario usuarioBuscado = s.GetUsuarioPorEmail(Email);
                if (usuarioBuscado.Contrasenia.Equals(Contrasenia))
                {
                    HttpContext.Session.SetInt32("idUsuarioLogueado", usuarioBuscado.Id);
                    HttpContext.Session.SetString("tipoUsuarioLogueado", usuarioBuscado.GetTipo());
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msgError = "El usuario y la contraseña no coinciden";
                    return View();
                }

            }
            catch (Exception e)
            {
                ViewBag.msgError = e.Message;
                return View();
            }

        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrarse(Miembro m)
        {
            try
            {
                s.AltaUsuario(m);
                ViewBag.msgOk = "Se ha registrado correctamente";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.msgError = e.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            string? rolUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolUsuarioLogueado != null)
            {
                HttpContext.Session.Clear();
                TempData.Clear(); //Intento de eliminar loa TempData al cerrar sesión
            }
            return RedirectToAction("Index");
        }

    }
}