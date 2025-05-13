using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace IUWebApp.Controllers
{
    public class PublicacionController : Controller
    {
        Sistema s = Sistema.Instancia();

        public IActionResult Index() //Mostramos los Posts habilitados para el usuario logueado, (si es Admin mostramos todos)
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado != null)
            {
                if (rolLogueado == "Administrador")
                {
                    return View(s.GetPosts()); //todos
                }
                else
                {
                    int? idLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                    Miembro m = (Miembro)s.GetUsuarioLogueado(idLogueado); //miembro asociado al usuario logueado
                    if (m.Bloqueado)
                    {
                        TempData["bloqueado"] = "Bloqueado";
                    }
                    return View(s.GetPostsHabilitados(idLogueado));
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Buscar() //Buscador de Publicaciones 
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado == "Miembro")
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Buscar(string buscar, int numero) //Filtramos las publicaciones segun los datos que brinda el usuario
        {
            if (!String.IsNullOrWhiteSpace(buscar))
            {
                string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
                if (rolLogueado == "Miembro")
                {
                    int? idLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                    Usuario u = s.GetUsuarioLogueado(idLogueado);
                    Miembro m = (Miembro)u;
                    if (m.Bloqueado)
                    {
                        TempData["bloqueado"] = "Bloqueado";
                    }
                    return View(s.GetPublicacionesFiltradas(buscar, numero, idLogueado));
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = "El texto no debe ser vacío";
                return View();
            }
        }

        public IActionResult Edit(int idPost) //Mostramos la vista para confirmacion de baneo de Post
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado == "Administrador")
            {
                return View(s.GetPostPorId(idPost));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(bool Check, int idPost) //Realizamos el baneo del post
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado == "Administrador")
            {
                if (Check)
                {
                    int? idUsuarioLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                    Usuario a = s.GetUsuarioLogueado(idUsuarioLogueado);
                    Post postBuscado = s.GetPostPorId(idPost);

                    Administrador admin = (Administrador)a;
                    admin.CensurarPost(postBuscado);
                }
                else
                {
                    ViewBag.msg = "Debe seleccionar la opción para banear";
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(int idPost) //Mostramos los comentarios habilitados para el miembro/admin logueado segun el idPost
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado != null)
            {
                Post p = s.GetPostPorId(idPost);
                ViewBag.Post = p;
                if (rolLogueado == "Administrador")
                {
                    return View(p.GetComentarios());
                }
                else
                {
                    int? idLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                    Miembro m = (Miembro)s.GetUsuarioLogueado(idLogueado);
                    if (m.Bloqueado)
                    {
                        TempData["bloqueado"] = "Bloqueado";
                    }
                    return View(s.GetComentariosHabilitados(idPost, idLogueado));
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Details(int idPost, string titulo, string texto, bool check) // Nuevo Comentario a un Post
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado == "Miembro")
            {
                try
                {
                    int? idLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                    Usuario u = s.GetUsuarioLogueado(idLogueado);
                    Miembro m = s.GetMiembroPorEmail(u.Email);
                    Post p = s.GetPostPorId(idPost);
                    if (!string.IsNullOrWhiteSpace(titulo) || !string.IsNullOrWhiteSpace(titulo))
                    {
                        s.NuevoComentario(m, titulo, texto, check, new List<Reaccion>(), p);
                        TempData["msgComentario"] = "Comentario publicado!";
                    }
                    else
                    {
                        TempData["msgComentarioError"] = "El titulo y el comentario son obligatorios, no pueden ser vacíos";
                    }
                    return RedirectToAction("Details", "Publicacion", new { idPost = p.Id });

                }
                catch (Exception e)
                {
                    TempData["msgComentarioError"] = e.Message;
                    return RedirectToAction("Details");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create(int idPublicacion, string reaccion, int idPosteo) // Nueva reaccion a Post o Comentario
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado == "Miembro")
            {
                Publicacion p = s.GetPublicacionPorId(idPublicacion);
                int? idLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                Miembro m = (Miembro)s.GetUsuarioLogueado(idLogueado);
                try
                {
                    s.NuevaReaccion(reaccion, m, p);
                }
                catch (Exception e)
                {
                    TempData["errorReaccion"] = e.Message;
                }
                
                //Agregar link de reacciones? 
                //if (p.GetTipo() == "Post" || idPosteo == 0) //El idPosteo será 0 cuando el tipo sea Post y Cuando sea Comentario en la vista Buscar, de lo contrario se va al else cuando es Comentario llamado desde la vista Details
                if (p.GetTipo() == "Post")
                {
                    //return RedirectToAction("Buscar"); //Agregando Reacciones en el buscador deberíamos evaluar doble retorno para recargar la página dependiendo de la vista en que haya sido llamado el metodo
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Details", "Publicacion", new { idPost = idPosteo });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreatePost()
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado == "Miembro")
            {
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public IActionResult CreatePost(string Titulo, string Texto, string Imagen, bool Privado) //Nuevo Post
        {
            string? rolLogueado = HttpContext.Session.GetString("tipoUsuarioLogueado");
            if (rolLogueado == "Miembro")
            {
                try
                {
                    int? idLogueado = HttpContext.Session.GetInt32("idUsuarioLogueado");
                    Usuario u = s.GetUsuarioLogueado(idLogueado);
                    Miembro m = s.GetMiembroPorEmail(u.Email);
                    s.NuevoPost(m, Titulo, Texto, Imagen, Privado, false, new List<Reaccion>());
                    TempData["msgPublicacion"] = "Posteo Realizado!";
                    return RedirectToAction("CreatePost", "Publicacion");

                }
                catch (Exception e)
                {
                    TempData["msgPublicacionError"] = e.Message;
                    return RedirectToAction("CreatePost", "Publicacion");
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
