using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace IUWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema s = Sistema.Instancia();

        public IActionResult Index() //Mostramos los Miembros habilitados para el usuario logueado, (si es Admin mostramos todos)
        {
            int? idUsuarioLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
            if (idUsuarioLogueado != null)
            {
                string? tipoUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");

                if (tipoUsuarioLogueado == "Administrador")
                {
                    return View(s.GetMiembros());
                }
                else
                {
                    return View(s.GetMiembrosHabilitados(idUsuarioLogueado));
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Edit(string emailMiembro)
        {
            string? tipoUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (tipoUsuarioLogueado == "Administrador")
            {
                return View(s.GetMiembroPorEmail(emailMiembro));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Edit(bool Check, string emailMiembro)
        {
            string? tipoUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (tipoUsuarioLogueado == "Administrador")
            {
                if (Check)
                {
                    int? idUsuarioLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                    Usuario a = s.GetUsuarioLogueado(idUsuarioLogueado);
                    Miembro miembroBuscado = s.GetMiembroPorEmail(emailMiembro);

                    Administrador admin = (Administrador)a;
                    admin.BloquearUsuario(miembroBuscado);
                }
                else
                {
                    ViewBag.msg = "Debe seleccionar la opción para bloquear";
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public IActionResult Solicitudes()
        {
            int? idUsuarioLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
            string? tipoUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");

            if (tipoUsuarioLogueado == "Miembro")
            {
                return View(s.GetInvitacionesMiembro(idUsuarioLogueado));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ProcesarSolicitud(string respuesta, int idInvitacion)
        {
            string? tipoUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (tipoUsuarioLogueado == "Miembro")
            {
                int? idUsuarioLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                Usuario u = s.GetUsuarioLogueado(idUsuarioLogueado);
                Miembro m = (Miembro)u;
                Invitacion invitacion = s.GetInvitacionPorID(idInvitacion);
                try
                {
                    if (respuesta == "aceptar")
                    {
                        m.AceptarSolicitud(invitacion);
                        TempData["solicitud"] = $"Has aceptado la solicitud de {invitacion.MiembroSolicitante.Nombre} {invitacion.MiembroSolicitante.Apellido}";
                    }
                    else
                    {
                        m.RechazarSolicitud(invitacion);
                        TempData["solicitud"] = $"Has rechazado la solicitud de {invitacion.MiembroSolicitante.Nombre} {invitacion.MiembroSolicitante.Apellido}";
                    }
                    return RedirectToAction("Solicitudes");
                }
                catch (Exception e)
                {
                    TempData["solicitud"] = e.Message;
                    return RedirectToAction("Solicitudes");
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Solicitar(string emailMiembro)
        {
            string? tipoUsuarioLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if(tipoUsuarioLogueado == "Miembro")
            {
                int? idLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                Usuario u = s.GetUsuarioLogueado(idLogueado);
                Miembro miembroSolicitante = (Miembro)u;
                Miembro miembroSolicitado = s.GetMiembroPorEmail(emailMiembro);
                try
                {
                    if (!s.MiembroTieneInvitacionPendiente(miembroSolicitante, miembroSolicitado)) /* && !miembroSolicitante.Bloqueado Lo podemos agregar pero no nos tira el mensaje personalizado del Exception*/
                    {
                        s.NuevaInvitacion(miembroSolicitante, miembroSolicitado);
                        TempData["solicitudOk"] = $"Solicitud Enviada a {miembroSolicitado.Nombre} {miembroSolicitado.Apellido}";
                    }
                    return View("Index", s.GetMiembrosHabilitados(idLogueado));
                }
                catch (Exception e)
                {
                    TempData["solicitudError"] = e.Message;
                    return View("Index", s.GetMiembrosHabilitados(idLogueado));
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}