using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Sistema
    {
        private List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Invitacion> _invitaciones = new List<Invitacion>();
        private List<Usuario> _usuarios = new List<Usuario>();

        #region SINGLETON

        private static Sistema _instancia;
        public static Sistema Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new Sistema();
            }
            return _instancia;
        }

        #endregion
        private Sistema()
        {
            PrecargarDatos();
        }

        #region PRECARGAS
        //Precargamos todos los datos necesarios separandolos en 3 metodos privados para una mayor legibilidad, evitar perdida de datos y seguridad de los mismos.
        public void PrecargarDatos()
        {
            try
            {
                PrecargarUsuarios();
                PrecargarPublicaciones();
                PrecargarInvitaciones();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Precarga de Usuarios (miembro y administrador), utilizamos metodo AgregarUsuario para dar alta a nuestra lista de Usuarios del Sistema.
        private void PrecargarUsuarios()
        {
            try
            {
                NuevoMiembro("Rodrigo", "Cristaldo", "13/02/1995", "rodrigo@gmail.com", "rC1234");
                NuevoMiembro("Angel", "Vaz", "02/07/1997", "angel@gmail.com", "aV1234");
                NuevoMiembro("Edinson", "Cavani", "14/02/1987", "edi@gmail.com", "eC1234");
                NuevoMiembro("Diego", "Forlan", "19/05/1979", "forlan@gmail.com", "dF1234");
                NuevoMiembro("Sergio", "Ramos", "30/03/1986", "sergio@gmail.com", "sR1234");
                NuevoMiembro("Robert", "Lewandowski", "21/08/1988", "robert@gmail.com", "rL1234");
                NuevoMiembro("Cristiano", "Ronaldo", "05/02/1985", "cr7@gmail.com", "cR1234");
                NuevoMiembro("Neymar", "Santos", "05/02/1992", "neymar@gmail.com", "nS1234");
                NuevoMiembro("Luis", "Suarez", "24/01/1987", "lucho@gmail.com", "lS1234");
                NuevoMiembro("Lionel", "Messi", "24/06/1987", "messi@gmail.com", "lM1234");
                NuevoMiembro("Pato", "Sosa", "02/06/1987", "patososa@gmail.com", "pS1234");
                
                Administrador admin1 = new Administrador("admin1@gmail.com", "Admin4321");
                AltaUsuario(admin1);
            }
            catch (Exception e)
            {
                //Mostramos un mensaje de error en caso de que por alguna razon no se den de alta.

                throw new Exception(e.Message);
            }
        }

        //Precarga de las Publicaciones (post,comentario) y Reacciones a las mismas, utilizando los metodos de dar alta a nuestras listas de sistema.
        private void PrecargarPublicaciones()
        {
            try
            {
                // Creamos 5 Publicaciones de tipo Post
                Post po1 = new Post("balonDeOro.JPG", new List<Comentario>(), false, GetMiembroPorEmail("rodrigo@gmail.com"), new DateTime(2023, 09, 28), "Rivales", "Lionel Messi a un paso de un nuevo balón de oro, se alargará la diferencia con Cristiano Ronaldo?", true, new List<Reaccion>());
                Post po2 = new Post("messiEnUsa.jpg", new List<Comentario>(), false, GetMiembroPorEmail("messi@gmail.com"), new DateTime(2023, 09, 20), "El GOAT en USA", "Luego de un mal sabor de boca en PSG se ve un brillante desempeño de Lionel Messi en el Inter de Miami", true, new List<Reaccion>());
                Post po3 = new Post("fichajesEuropa.png", new List<Comentario>(), false, GetMiembroPorEmail("lucho@gmail.com"), new DateTime(2023, 09, 25), "Mercado de fichajes", "Los clubes de la elite europea realizan movimientos estratégicos", true, new List<Reaccion>());
                Post po4 = new Post("UCL2023.jpg", new List<Comentario>(), false, GetMiembroPorEmail("angel@gmail.com"), new DateTime(2023, 09, 15), "UEFA Champions League", "Clubes poderosos compiten por el título europeo", true, new List<Reaccion>());
                Post po5 = new Post("var.png", new List<Comentario>(), false, GetMiembroPorEmail("robert@gmail.com"), new DateTime(2023, 09, 05), "VAR", "2023: El VAR continúa transformando el fútbol moderno", true, new List<Reaccion>());

                // Damos de alta a los Posts a nuestra lista de Publicaciones del Sistema
                AltaPublicacion(po1);
                AltaPublicacion(po2);
                AltaPublicacion(po3);
                AltaPublicacion(po4);
                AltaPublicacion(po5);

                //Nuevos Post Obligatorio2

                NuevoPost(GetMiembroPorEmail("neymar@gmail.com"), "Gol Sorprendente", "¡Increíble momento en el campo!", "neymar_gol.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("robert@gmail.com"), "Jugada Maestra", "Deslumbrante habilidad en el partido", "robert_jugada.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("sergio@gmail.com"), "Victoria Asombrosa", "Celebrando una victoria épica", "sergio_victoria.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("cr7@gmail.com"), "Clásico Intenso", "Momentos emocionantes en el clásico", "cr7_clasico.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("patososa@gmail.com"), "Regate Asombroso", "¡No podrás creer este regate!", "patososa_regate.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("forlan@gmail.com"), "Gol de Chilena", "La chilena que dejó a todos boquiabiertos", "forlan_chilena.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("neymar@gmail.com"), "Remate Preciso", "Precisión en cada disparo al arco", "neymar_remate.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("robert@gmail.com"), "Duelo Intenso", "Partido emocionante hasta el último minuto", "robert_duelo.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("sergio@gmail.com"), "Fiesta en el Estadio", "La afición haciendo vibrar el estadio", "sergio_fiesta.jpg", false, false, new List<Reaccion>());
                NuevoPost(GetMiembroPorEmail("cr7@gmail.com"), "Debut Triunfal", "Un debut soñado en el mundo del fútbol", "cr7_debut.jpg", false, false, new List<Reaccion>());

                // Creamos 15 Publicaciones de tipo Comentario, 3 para cada Publicacion de tipo Post existente

                Comentario co1 = new Comentario(GetMiembroPorEmail("angel@gmail.com"), "Grandes", "Sus habilidades en el campo son asombrosas y verlos competir por este premio es simplemente épico.", false, new List<Reaccion>());
                Comentario co2 = new Comentario(GetMiembroPorEmail("lucho@gmail.com"), "Como dos fieras", "La eterna pulseada por el Balón de Oro!", false, new List<Reaccion>());
                Comentario co3 = new Comentario(GetMiembroPorEmail("neymar@gmail.com"), "Show", "No hay duda, Messi y Ronaldo saben cómo mantenernos en vilo. Este duelo por el Balón de Oro es un espectáculo en sí mismo.", false, new List<Reaccion>());

                Comentario co4 = new Comentario(GetMiembroPorEmail("cr7@gmail.com"), "Competencia", "Un fuerte abrazo para Leo en esta nueva etapa en Inter de Miami. La competencia siempre eleva nuestro juego y estoy seguro de que la vas a romper en la MLS", false, new List<Reaccion>());
                Comentario co5 = new Comentario(GetMiembroPorEmail("sergio@gmail.com"), "Que sigan los goles", "Estoy seguro de que vas a dejar huella en la MLS. Que sigan los éxitos y los goles, Leo", false, new List<Reaccion>());
                Comentario co6 = new Comentario(GetMiembroPorEmail("edi@gmail.com"), "Retos y Triunfos", "Que sigan los retos y los triunfos, admiración por tu talento", false, new List<Reaccion>());

                Comentario co7 = new Comentario(GetMiembroPorEmail("rodrigo@gmail.com"), "Partidazos", "Los movimientos del mercado de fichaje que estamos viendo son verdaderamente impresionantes. Prepárense para una temporada cargada de sorpresas y partidazos! ", false, new List<Reaccion>());
                Comentario co8 = new Comentario(GetMiembroPorEmail("forlan@gmail.com"), "Elite", "Es fascinante ver cómo los clubes de la élite están planificando sus futuras temporadas.", false, new List<Reaccion>());
                Comentario co9 = new Comentario(GetMiembroPorEmail("neymar@gmail.com"), "Espectáculo", "Un espectáculo deportivo sin igual. ", false, new List<Reaccion>());

                Comentario co10 = new Comentario(GetMiembroPorEmail("rodrigo@gmail.com"), "Momentos mágicos", "¡Que vivan los momentos mágicos de la Champions!", false, new List<Reaccion>());
                Comentario co11 = new Comentario(GetMiembroPorEmail("lucho@gmail.com"), "Nada se compara", "La lucha por el título europeo es un verdadero espectáculo, No hay nada como la Champions!", false, new List<Reaccion>());
                Comentario co12 = new Comentario(GetMiembroPorEmail("neymar@gmail.com"), "Futuro", "La Champions de este año es el escenario donde las futuras leyendas del fútbol están forjando su destino", false, new List<Reaccion>());

                Comentario co13 = new Comentario(GetMiembroPorEmail("cr7@gmail.com"), "Defectos del VAR", "Afecta la espontaneidad y la emoción del juego. Las celebraciones intensas tras un gol se ven frenadas por la revisión constante", false, new List<Reaccion>());
                Comentario co14 = new Comentario(GetMiembroPorEmail("forlan@gmail.com"), "Clave", "La clave está en perfeccionar su uso para evitar malentendidos y mantener el juego justo y emocionante.", false, new List<Reaccion>());
                Comentario co15 = new Comentario(GetMiembroPorEmail("edi@gmail.com"), "Progreso", "Bienvenido sea el progreso tecnológico en nuestro deporte favorito", false, new List<Reaccion>());

                //Asociamos los comentarios a cada publicacion.

                po1.AltaComentario(co1);
                AltaPublicacion(co1);
                po1.AltaComentario(co2);
                AltaPublicacion(co2);
                po1.AltaComentario(co3);
                AltaPublicacion(co3);

                po2.AltaComentario(co4);
                AltaPublicacion(co4);
                po2.AltaComentario(co5);
                AltaPublicacion(co5);
                po2.AltaComentario(co6);
                AltaPublicacion(co6);

                po3.AltaComentario(co7);
                AltaPublicacion(co7);
                po3.AltaComentario(co8);
                AltaPublicacion(co8);
                po3.AltaComentario(co9);
                AltaPublicacion(co9);

                po4.AltaComentario(co10);
                AltaPublicacion(co10);
                po4.AltaComentario(co11);
                AltaPublicacion(co11);
                po4.AltaComentario(co12);
                AltaPublicacion(co12);

                po5.AltaComentario(co13);
                AltaPublicacion(co13);
                po5.AltaComentario(co14);
                AltaPublicacion(co14);
                po5.AltaComentario(co15);
                AltaPublicacion(co15);

                //Nuevos Comentarios Obligatorio2

                // Comentarios para la publicación de Neymar
                NuevoComentario(GetMiembroPorEmail("lucho@gmail.com"), "Increíble", "Excelente gol, Neymar!", false, new List<Reaccion>(), GetPostPorId(6));
                NuevoComentario(GetMiembroPorEmail("angel@gmail.com"), "Asombroso", "¡Qué habilidad increíble!", false, new List<Reaccion>(), GetPostPorId(6));
                NuevoComentario(GetMiembroPorEmail("messi@gmail.com"), "Sorprendente", "Neymar siempre sorprendiéndonos", false, new List<Reaccion>(), GetPostPorId(6));

                // Comentarios para la publicación de Robert
                NuevoComentario(GetMiembroPorEmail("sergio@gmail.com"), "Increíble", "Impresionante jugada, Robert!", false, new List<Reaccion>(), GetPostPorId(7));
                NuevoComentario(GetMiembroPorEmail("rodrigo@gmail.com"), "Asombroso", "¡Esa habilidad es de otro nivel!", false, new List<Reaccion>(), GetPostPorId(7));
                NuevoComentario(GetMiembroPorEmail("cr7@gmail.com"), "Impresionante", "Robert, siempre dejándonos sin palabras", false, new List<Reaccion>(), GetPostPorId(7));

                // Comentarios para la publicación de Sergio
                NuevoComentario(GetMiembroPorEmail("patososa@gmail.com"), "Felicidades", "¡Felicidades por la victoria, Sergio!", false, new List<Reaccion>(), GetPostPorId(8));
                NuevoComentario(GetMiembroPorEmail("lucho@gmail.com"), "Emocionante", "Esa celebración lo dice todo", false, new List<Reaccion>(), GetPostPorId(8));
                NuevoComentario(GetMiembroPorEmail("forlan@gmail.com"), "Líder", "Sergio, un líder en el campo", false, new List<Reaccion>(), GetPostPorId(8));

                // Comentarios para la publicación de CR7
                NuevoComentario(GetMiembroPorEmail("neymar@gmail.com"), "Épico", "Ese clásico estuvo increíble, CR7!", false, new List<Reaccion>(), GetPostPorId(9));
                NuevoComentario(GetMiembroPorEmail("lucho@gmail.com"), "Épico", "Momentos épicos en cada partido", false, new List<Reaccion>(), GetPostPorId(9));
                NuevoComentario(GetMiembroPorEmail("messi@gmail.com"), "El Mejor", "CR7, el mejor jugador del mundo", false, new List<Reaccion>(), GetPostPorId(9));

                // Comentarios para la publicación de Patososa
                NuevoComentario(GetMiembroPorEmail("forlan@gmail.com"), "Asombroso", "Regate asombroso, Patososa!", false, new List<Reaccion>(), GetPostPorId(10));
                NuevoComentario(GetMiembroPorEmail("sergio@gmail.com"), "Único", "¡Esa habilidad en el campo es única!", false, new List<Reaccion>(), GetPostPorId(10));
                NuevoComentario(GetMiembroPorEmail("neymar@gmail.com"), "Rey del Regate", "Patososa, el rey del regate", false, new List<Reaccion>(), GetPostPorId(10));

                // Comentarios para la publicación de Forlán
                NuevoComentario(GetMiembroPorEmail("lucho@gmail.com"), "Impresionante", "Chilena impresionante, Forlán!", false, new List<Reaccion>(), GetPostPorId(11));
                NuevoComentario(GetMiembroPorEmail("robert@gmail.com"), "Demostrando Calidad", "Forlán, siempre demostrando su calidad", false, new List<Reaccion>(), GetPostPorId(11));
                NuevoComentario(GetMiembroPorEmail("sergio@gmail.com"), "Ícono", "Forlán, un ícono del fútbol", false, new List<Reaccion>(), GetPostPorId(11));

                // Comentarios para la publicación de Neymar
                NuevoComentario(GetMiembroPorEmail("patososa@gmail.com"), "Preciso", "Precisión en el remate, Neymar!", false, new List<Reaccion>(), GetPostPorId(12));
                NuevoComentario(GetMiembroPorEmail("forlan@gmail.com"), "Sin Aliento", "Neymar, siempre dejándonos sin aliento", false, new List<Reaccion>(), GetPostPorId(12));
                NuevoComentario(GetMiembroPorEmail("rodrigo@gmail.com"), "Maestro", "Neymar, un maestro del fútbol", false, new List<Reaccion>(), GetPostPorId(12));

                // Comentarios para la publicación de Robert
                NuevoComentario(GetMiembroPorEmail("messi@gmail.com"), "Intenso", "Duelo intenso, Robert!", false, new List<Reaccion>(), GetPostPorId(13));
                NuevoComentario(GetMiembroPorEmail("neymar@gmail.com"), "Entregando Todo", "Robert, siempre entregando todo en el campo", false, new List<Reaccion>(), GetPostPorId(13));
                NuevoComentario(GetMiembroPorEmail("sergio@gmail.com"), "Gran Partido", "Gran partido, Robert!", false, new List<Reaccion>(), GetPostPorId(13));

                // Comentarios para la publicación de Sergio
                NuevoComentario(GetMiembroPorEmail("robert@gmail.com"), "Presente", "La afición siempre presente, Sergio!", false, new List<Reaccion>(), GetPostPorId(14));
                NuevoComentario(GetMiembroPorEmail("lucho@gmail.com"), "El Alma", "Sergio, el alma del equipo", false, new List<Reaccion>(), GetPostPorId(14));
                NuevoComentario(GetMiembroPorEmail("patososa@gmail.com"), "Líder", "Sergio, un verdadero líder", false, new List<Reaccion>(), GetPostPorId(14));

                // Comentarios para la publicación de CR7
                NuevoComentario(GetMiembroPorEmail("rodrigo@gmail.com"), "Debut Soñado", "Debut soñado, CR7!", false, new List<Reaccion>(), GetPostPorId(15));
                NuevoComentario(GetMiembroPorEmail("sergio@gmail.com"), "Marcando la Diferencia", "CR7, siempre marcando la diferencia", false, new List<Reaccion>(), GetPostPorId(15));
                NuevoComentario(GetMiembroPorEmail("forlan@gmail.com"), "Inolvidable", "Esa primera vez en el campo, inolvidable", false, new List<Reaccion>(), GetPostPorId(15));


                //Precarga de Reacciones a Publicaciones

                Reaccion re1 = new Reaccion(Tipo.like, GetMiembroPorEmail("angel@gmail.com"));
                Reaccion re2 = new Reaccion(Tipo.dislike, GetMiembroPorEmail("rodrigo@gmail.com"));
                Reaccion re3 = new Reaccion(Tipo.like, GetMiembroPorEmail("lucho@gmail.com"));

                Reaccion re4 = new Reaccion(Tipo.like, GetMiembroPorEmail("neymar@gmail.com"));
                Reaccion re5 = new Reaccion(Tipo.like, GetMiembroPorEmail("messi@gmail.com"));
                Reaccion re6 = new Reaccion(Tipo.dislike, GetMiembroPorEmail("edi@gmail.com"));

                Reaccion re7 = new Reaccion(Tipo.dislike, GetMiembroPorEmail("angel@gmail.com"));
                Reaccion re8 = new Reaccion(Tipo.like, GetMiembroPorEmail("rodrigo@gmail.com"));
                Reaccion re9 = new Reaccion(Tipo.like, GetMiembroPorEmail("lucho@gmail.com"));

                Reaccion re10 = new Reaccion(Tipo.like, GetMiembroPorEmail("neymar@gmail.com"));
                Reaccion re11 = new Reaccion(Tipo.dislike, GetMiembroPorEmail("messi@gmail.com"));
                Reaccion re12 = new Reaccion(Tipo.like, GetMiembroPorEmail("edi@gmail.com"));

                //Agregamos 3 reacciones a las lista de reacciones de 2 publicaciones de tipo Post y 2 publicaciones de tipo Comentario mediante un metodo.

                po2.AgregarReaccion(re1);
                po2.AgregarReaccion(re2);
                po2.AgregarReaccion(re3);

                po3.AgregarReaccion(re4);
                po3.AgregarReaccion(re5);
                po3.AgregarReaccion(re6);

                co9.AgregarReaccion(re7);
                co9.AgregarReaccion(re8);
                co9.AgregarReaccion(re9);

                co13.AgregarReaccion(re10);
                co13.AgregarReaccion(re11);
                co13.AgregarReaccion(re12);


                //Nuevas Reacciones Obligatorio2

                // Reacciones de tipo "like"
                NuevaReaccion("like", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(1)); //rod
                NuevaReaccion("like", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(2)); //mess
                NuevaReaccion("like", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(3)); //lucho
                NuevaReaccion("like", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(4)); //ang
                NuevaReaccion("like", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(5)); //robert
                NuevaReaccion("like", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(6));
                NuevaReaccion("like", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(7));
                NuevaReaccion("like", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(8));
                NuevaReaccion("like", GetMiembroPorEmail("forlan@gmail.com"), GetPublicacionPorId(9));
                NuevaReaccion("like", GetMiembroPorEmail("patososa@gmail.com"), GetPublicacionPorId(10));
                NuevaReaccion("like", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(11));
                NuevaReaccion("like", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(12));
                NuevaReaccion("like", GetMiembroPorEmail("neymar@gmail.com"), GetPublicacionPorId(13));
                NuevaReaccion("like", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(14));
                NuevaReaccion("like", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(15));
                NuevaReaccion("like", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(16));
                NuevaReaccion("like", GetMiembroPorEmail("cr7@gmail.com"), GetPublicacionPorId(17));
                NuevaReaccion("like", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(18));
                NuevaReaccion("like", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(19));
                NuevaReaccion("like", GetMiembroPorEmail("forlan@gmail.com"), GetPublicacionPorId(20));
                NuevaReaccion("like", GetMiembroPorEmail("patososa@gmail.com"), GetPublicacionPorId(21));
                NuevaReaccion("like", GetMiembroPorEmail("neymar@gmail.com"), GetPublicacionPorId(22));
                NuevaReaccion("like", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(23));
                NuevaReaccion("like", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(24));
                NuevaReaccion("like", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(25));
                NuevaReaccion("like", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(26));
                NuevaReaccion("like", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(27));
                NuevaReaccion("like", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(28));
                NuevaReaccion("like", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(29));
                NuevaReaccion("like", GetMiembroPorEmail("forlan@gmail.com"), GetPublicacionPorId(30));
                NuevaReaccion("like", GetMiembroPorEmail("patososa@gmail.com"), GetPublicacionPorId(31));
                NuevaReaccion("like", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(32));
                NuevaReaccion("like", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(33));
                NuevaReaccion("like", GetMiembroPorEmail("neymar@gmail.com"), GetPublicacionPorId(34));
                NuevaReaccion("like", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(35));
                NuevaReaccion("like", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(36));
                NuevaReaccion("like", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(37));
                NuevaReaccion("like", GetMiembroPorEmail("cr7@gmail.com"), GetPublicacionPorId(38));
                NuevaReaccion("like", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(39));
                NuevaReaccion("like", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(40));
                NuevaReaccion("like", GetMiembroPorEmail("forlan@gmail.com"), GetPublicacionPorId(41));
                NuevaReaccion("like", GetMiembroPorEmail("patososa@gmail.com"), GetPublicacionPorId(42));
                NuevaReaccion("like", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(43));
                NuevaReaccion("like", GetMiembroPorEmail("neymar@gmail.com"), GetPublicacionPorId(44));
                NuevaReaccion("like", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(45));
                NuevaReaccion("like", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(46));
                NuevaReaccion("like", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(47));
                NuevaReaccion("like", GetMiembroPorEmail("cr7@gmail.com"), GetPublicacionPorId(48));
                NuevaReaccion("like", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(49));
                NuevaReaccion("like", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(50));
                NuevaReaccion("like", GetMiembroPorEmail("forlan@gmail.com"), GetPublicacionPorId(51));
                NuevaReaccion("like", GetMiembroPorEmail("patososa@gmail.com"), GetPublicacionPorId(52));
                NuevaReaccion("like", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(53));
                NuevaReaccion("like", GetMiembroPorEmail("neymar@gmail.com"), GetPublicacionPorId(54));
                NuevaReaccion("like", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(55));
                NuevaReaccion("like", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(56));
                NuevaReaccion("like", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(57));
                NuevaReaccion("like", GetMiembroPorEmail("cr7@gmail.com"), GetPublicacionPorId(58));
                NuevaReaccion("like", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(59));
                NuevaReaccion("like", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(60));

                // Reacciones de tipo "dislike"

                NuevaReaccion("dislike", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(6));
                NuevaReaccion("dislike", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(7));
                NuevaReaccion("dislike", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(8));
                NuevaReaccion("dislike", GetMiembroPorEmail("patososa@gmail.com"), GetPublicacionPorId(9));
                NuevaReaccion("dislike", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(10));
                NuevaReaccion("dislike", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(11));
                NuevaReaccion("dislike", GetMiembroPorEmail("neymar@gmail.com"), GetPublicacionPorId(12));
                NuevaReaccion("dislike", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(13));
                NuevaReaccion("dislike", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(14));
                NuevaReaccion("dislike", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(15));
                NuevaReaccion("dislike", GetMiembroPorEmail("cr7@gmail.com"), GetPublicacionPorId(16));
                NuevaReaccion("dislike", GetMiembroPorEmail("angel@gmail.com"), GetPublicacionPorId(17));
                NuevaReaccion("dislike", GetMiembroPorEmail("sergio@gmail.com"), GetPublicacionPorId(18));
                NuevaReaccion("dislike", GetMiembroPorEmail("forlan@gmail.com"), GetPublicacionPorId(19));
                NuevaReaccion("dislike", GetMiembroPorEmail("patososa@gmail.com"), GetPublicacionPorId(20));
                NuevaReaccion("dislike", GetMiembroPorEmail("neymar@gmail.com"), GetPublicacionPorId(21));
                NuevaReaccion("dislike", GetMiembroPorEmail("lucho@gmail.com"), GetPublicacionPorId(22));
                NuevaReaccion("dislike", GetMiembroPorEmail("robert@gmail.com"), GetPublicacionPorId(23));
                NuevaReaccion("dislike", GetMiembroPorEmail("messi@gmail.com"), GetPublicacionPorId(24));
                NuevaReaccion("dislike", GetMiembroPorEmail("rodrigo@gmail.com"), GetPublicacionPorId(25));


            }
            catch (Exception e)
            {
                //Mostramos un mensaje en caso de error 
                throw new Exception(e.Message);
            }
        }

        //Precargamos las invitaciones con sus respectivas altas.
        private void PrecargarInvitaciones()
        {
            try
            {
                //Creamos  invitaciones en sus 3 estados posibles
                Invitacion i1 = new Invitacion(GetMiembroPorEmail("rodrigo@gmail.com"), GetMiembroPorEmail("angel@gmail.com"), EstadoInvitacion.Aprobada);
                Invitacion i2 = new Invitacion(GetMiembroPorEmail("rodrigo@gmail.com"), GetMiembroPorEmail("messi@gmail.com"), EstadoInvitacion.Aprobada);
                Invitacion i3 = new Invitacion(GetMiembroPorEmail("rodrigo@gmail.com"), GetMiembroPorEmail("lucho@gmail.com"), EstadoInvitacion.Aprobada);
                Invitacion i4 = new Invitacion(GetMiembroPorEmail("messi@gmail.com"), GetMiembroPorEmail("lucho@gmail.com"), EstadoInvitacion.Aprobada);
                Invitacion i5 = new Invitacion(GetMiembroPorEmail("angel@gmail.com"), GetMiembroPorEmail("lucho@gmail.com"), EstadoInvitacion.Aprobada);
                Invitacion i6 = new Invitacion(GetMiembroPorEmail("angel@gmail.com"), GetMiembroPorEmail("robert@gmail.com"), EstadoInvitacion.Pendiente_Aprobacion);
                Invitacion i7 = new Invitacion(GetMiembroPorEmail("robert@gmail.com"), GetMiembroPorEmail("lucho@gmail.com"), EstadoInvitacion.Pendiente_Aprobacion);
                Invitacion i8 = new Invitacion(GetMiembroPorEmail("lucho@gmail.com"), GetMiembroPorEmail("neymar@gmail.com"), EstadoInvitacion.Rechazada);
                Invitacion i9 = new Invitacion(GetMiembroPorEmail("robert@gmail.com"), GetMiembroPorEmail("neymar@gmail.com"), EstadoInvitacion.Rechazada);
                Invitacion i10 = new Invitacion(GetMiembroPorEmail("messi@gmail.com"), GetMiembroPorEmail("robert@gmail.com"), EstadoInvitacion.Pendiente_Aprobacion);

                //Damos alta a las invitaciones a la lista de Invitaciones del Sistema
                AltaInvitacion(i1);
                AltaInvitacion(i2);
                AltaInvitacion(i3);
                AltaInvitacion(i4);
                AltaInvitacion(i5);
                AltaInvitacion(i6);
                AltaInvitacion(i7);
                AltaInvitacion(i8);
                AltaInvitacion(i9);
                AltaInvitacion(i10);

                // Invitaciones utilizando GetMiembroPorEmail
                NuevaInvitacion(GetMiembroPorEmail("neymar@gmail.com"), GetMiembroPorEmail("messi@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("angel@gmail.com"), GetMiembroPorEmail("messi@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("cr7@gmail.com"), GetMiembroPorEmail("robert@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("sergio@gmail.com"), GetMiembroPorEmail("rodrigo@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("forlan@gmail.com"), GetMiembroPorEmail("patososa@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("lucho@gmail.com"), GetMiembroPorEmail("sergio@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("messi@gmail.com"), GetMiembroPorEmail("forlan@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("robert@gmail.com"), GetMiembroPorEmail("neymar@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("rodrigo@gmail.com"), GetMiembroPorEmail("cr7@gmail.com"));
                NuevaInvitacion(GetMiembroPorEmail("patososa@gmail.com"), GetMiembroPorEmail("angel@gmail.com"));


                //Mediante este metodo generamos el vinculo de amistad en los casos en que el estado de la invitacion sea 'Aprobada'.
                GenerarVinculosDeAmistad();
            }
            catch (Exception e)
            {
                //Mostramos un mensaje en caso de error.
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region ALTAS

        //Desarrollamos los metodos de altas para (Usuario, Publicacion, Invitacion). Consta de agregar el objeto a la lista correspondiente del sistema luego de sortear validaciones especificas para cada uno.

        public void AltaUsuario(Usuario u)
        {
            try
            {
                if (u != null)
                {
                    //Validamos los datos.
                    u.Validar();
                    //Agregamos a la lista de usuarios del sistema
                    _usuarios.Add(u);
                }
            }
            catch (Exception e)
            {
                //Mensaje de error en caso que falle.
                throw new Exception(e.Message);
            }
        }

        public void AltaPublicacion(Publicacion p)
        {
            try
            {
                if (p != null)
                {
                    //Validamos los datos.
                    p.Validar();
                    //Agregamos a la lista de publicaciones del sistema.
                    _publicaciones.Add(p);
                }
            }
            catch (Exception e)
            {
                //Mensaje de error en caso que falle.
                throw new Exception(e.Message);
            }
        }

        public void AltaInvitacion(Invitacion i)
        {
            try
            {
                if (i != null)
                {
                    //Validamos los datos.
                    i.Validar();
                    //Agregamos a la lista de publicaciones del sistema.
                    _invitaciones.Add(i);
                }
            }
            catch (Exception e)
            {
                //Mensaje de error en caso que falle.
                throw new Exception(e.Message);
            }
        }

        //Metodo para crear un nuevo miembro.
        public void NuevoMiembro(string nombre, string apellido, string fecha, string email, string contrasenia)
        {
            try
            {
                foreach (Usuario u in _usuarios)
                {
                    if (u.Email == email)
                    {
                        //Mensaje si existe otro usuario con el mismo email.
                        throw new Exception("Ya existe un usuario con ese Email en el sistema");
                    }
                }

                if (string.IsNullOrEmpty(nombre))
                {
                    //Mensaje de error en caso de que el nombre este vacio.

                    throw new Exception("El nombre no puede estar vacio");
                }

                if (string.IsNullOrEmpty(apellido))
                {
                    //Mensaje de error en caso de que el apellido este vacio.

                    throw new Exception("El apellido no puede estar vacio");

                }

                if (string.IsNullOrEmpty(fecha))
                {
                    //Mensaje de error en caso de que la fecha este vacia.

                    throw new Exception("La fecha de nacimiento no puede ser vacia");
                }

                if (!ValidarFormatoFecha(fecha))
                {
                    //Mensaje de error en caso de que la fecha no sea del formato indicado .

                    throw new Exception("La fecha de nacimiento debe tener formato DD/MM/AAAA");
                }

                if (string.IsNullOrEmpty(email))
                {
                    //Mensaje de error en caso de que el Email este vacio.
                    throw new Exception("El mail no puede ser vacio");
                }
                if (string.IsNullOrEmpty(contrasenia))
                {
                    //Mensaje de error en caso de que la contraseña este vacia.
                    throw new Exception("La contrasenia no puede ser vacia");
                }
                //Aqui se crea el nuevo miembro.
                DateTime fechaForm = ConvertirStringADateTime(fecha);
                Miembro m1 = new Miembro(nombre, apellido, fechaForm, false, email, contrasenia);
                AltaUsuario(m1);
            }
            catch (Exception e)
            {
                //Mostramos los errores si es que surgen.
                throw new Exception(e.Message);
            }

        }

        public void NuevoComentario(Miembro m, string titulo, string texto, bool privado, List<Reaccion> reacciones, Post post)
        {
            try
            {
                if (m == null)
                {
                    //Mensaje de error en caso de que la fecha este vacia.

                    throw new Exception("Debe iniciar sesión para poder comentar");
                }

                if (m.Bloqueado)
                {
                    //Mensaje de error en caso de que la fecha este vacia.

                    throw new Exception("No puede comentar, está bloqueado");
                }
                if (post == null)
                {
                    //Mensaje de error en caso de que la fecha este vacia.

                    throw new Exception("Este Post no existe o ha sido eliminado");
                }
                if (post.Censurado)
                {
                    //Mensaje de error en caso de que la fecha este vacia.

                    throw new Exception("Este Post no se puede comentar ya que ha sido baneado");
                }

                if (string.IsNullOrEmpty(titulo))
                {
                    //Mensaje de error en caso de que el nombre este vacio.

                    throw new Exception("El titulo no puede estar vacio");
                }

                if (string.IsNullOrEmpty(texto))
                {
                    //Mensaje de error en caso de que el apellido este vacio.

                    throw new Exception("El comentario no puede estar vacio");

                }

                //Aqui se crea el nuevo comentario.
                Comentario c = new Comentario(m, titulo, texto, privado, reacciones);
                post.AltaComentario(c);
                AltaPublicacion(c);
            }
            catch (Exception e)
            {
                //Mostramos los errores si es que surgen.
                throw new Exception(e.Message);
            }
        }

        public void NuevoPost(Miembro m, string titulo, string texto, string imagen, bool privado, bool censurado, List<Reaccion> reacciones)
        {
            try
            {
                if (m == null)
                {
                    //Mensaje de error en caso de que la fecha este vacia.

                    throw new Exception("Debe iniciar sesión para poder publicar");
                }

                if (m.Bloqueado)
                {
                    //Mensaje de error en caso de que la fecha este vacia.

                    throw new Exception("No puede publicar, está bloqueado");
                }

                if (string.IsNullOrEmpty(titulo))
                {
                    //Mensaje de error en caso de que el nombre este vacio.

                    throw new Exception("El titulo no puede estar vacio");
                }

                if (string.IsNullOrEmpty(texto))
                {
                    //Mensaje de error en caso de que el apellido este vacio.

                    throw new Exception("El contenido no puede estar vacio");

                }

                if (string.IsNullOrEmpty(imagen))
                {
                    //Mensaje de error en caso de que el apellido este vacio.

                    throw new Exception("La imagen no puede estar vacia");

                }

                //Aqui se crea el nuevo comentario.
                Post p = new Post(imagen, new List<Comentario>(), censurado, m, DateTime.Now, titulo, texto, privado, reacciones);
                AltaPublicacion(p);
            }
            catch (Exception e)
            {
                //Mostramos los errores si es que surgen.
                throw new Exception(e.Message);
            }
        }

        public void NuevaInvitacion(Miembro miembroSolicitante, Miembro miembroSolicitado)
        {
            try
            {
                if (miembroSolicitante == null)
                {
                    //Mensaje de error en caso de que el miembro solicitante no este en el sistema // no esté logueado

                    throw new Exception("Debe iniciar sesión para poder enviar solicitudes");
                }

                if (miembroSolicitado == null)
                {
                    //Mensaje de error en caso de que el miembro solicitado no este en el sistema

                    throw new Exception("Miembro solicitado no está en el sistema");
                }

                if (miembroSolicitante.Bloqueado)
                {
                    //Mensaje de error en caso de que el miembro solicitante este bloqueado

                    throw new Exception("No puede enviar solicitudes, está bloqueado");
                }

                if (miembroSolicitado.Bloqueado)
                {
                    //Mensaje de error en caso de que el miembro solicitado este bloqueado

                    throw new Exception("El destinatario no puede recibir solicitudes, está bloqueado");
                }

                //Aqui se crea la nueva invitacion.
                Invitacion i = new Invitacion(miembroSolicitante, miembroSolicitado, EstadoInvitacion.Pendiente_Aprobacion);
                AltaInvitacion(i);
            }
            catch (Exception e)
            {
                //Mostramos los errores si es que surgen.
                throw new Exception(e.Message);
            }
        }

        public void NuevaReaccion(string tipoReaccion, Miembro m, Publicacion p)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tipoReaccion))
                {
                    //Mensaje de error en caso de que el miembro solicitante no este en el sistema // no esté logueado

                    throw new Exception("Seleccione una reaccion valida");
                }

                if (m == null)
                {
                    //Mensaje de error en caso de que el miembro solicitado no este en el sistema

                    throw new Exception("El miembro seleccionado no está en el sistema");
                }

                if (m.Bloqueado)
                {
                    //Mensaje de error en caso de que el miembro solicitante este bloqueado

                    throw new Exception("No puede reaccionar, está bloqueado");
                }

                if (p == null)
                {
                    //Mensaje de error en caso de que el miembro solicitado no este en el sistema

                    throw new Exception("La publicacion seleccionada no está en el sistema");
                }

                if (p.GetTipo() == "Post")
                {
                    Post post = (Post)p;
                    if (post.Censurado)
                    {
                        //Mensaje de error en caso de que el miembro solicitado este bloqueado

                        throw new Exception("El post no puede ser reaccionado, está baneado");
                    }
                }

                Tipo reac;
                if (tipoReaccion == "like")
                {
                    reac = Tipo.like;
                }
                else
                {
                    reac = Tipo.dislike;
                }
                //Aqui se crea la nueva reacción.
                Reaccion r = new Reaccion(reac, m);
                p.AgregarReaccion(r);
            }
            catch (Exception e)
            {
                //Mostramos los errores si es que surgen.
                throw new Exception(e.Message);
            }
        }

        //Metodo para generar vinculos de amistad.
        public void GenerarVinculosDeAmistad()
        {
            foreach (Invitacion i in _invitaciones)
            {
                //Verificamos las invitaciones entre 2 miembros que sean 'Aprobada'.
                if (i.Estado == EstadoInvitacion.Aprobada)
                {
                    //Agregamos a un miembro (Solicitante, Solicitado) a la lista de amigos del otro. Asi creamos el vinculo de amistad
                    i.MiembroSolicitado.AgregarAmigo(i.MiembroSolicitante);
                    i.MiembroSolicitante.AgregarAmigo(i.MiembroSolicitado);
                }
            }
        }

        #endregion ALTAS

        #region GETS

        //Con los metodos Get tenemos acceso a informacion especifica del sistema (listas).
        public List<Miembro> GetMiembros()
        {
            //Obtenemos la lista de miembros del sistema.
            List<Miembro> miembros = new List<Miembro>();
            foreach (Usuario u in _usuarios)
            {
                //Verificamos si el usuario es un Miembro.
                if (u != null && u.GetTipo() == "Miembro")
                {
                    Miembro miembroAux = u as Miembro;
                    miembros.Add(miembroAux);
                }
            }
            miembros.Sort();
            return miembros;
        }

        public List<Administrador> GetAdministradores()
        {
            //Obtenemos la lista de administradores del sistema.
            List<Administrador> administradores = new List<Administrador>();
            foreach (Usuario u in _usuarios)
            {
                //Verificamos si el usuario es un Administrador
                if (u != null && u.GetTipo() == "Administrador")
                {
                    Administrador adminAux = u as Administrador;
                    administradores.Add(adminAux);
                }
            }
            return administradores;
        }

        public List<Publicacion> GetPublicaciones()
        {
            //Obtenemos la lista de todas las publicaciones en el sistema
            return _publicaciones;
        }

        public List<Post> GetPosts()
        {
            //Obtenemos la lista de posts del sistema.
            List<Post> posts = new List<Post>();
            foreach (Publicacion p in _publicaciones)
            {
                //Verificamos si la publicacion es un Post.
                if (p != null && p.GetTipo() == "Post")
                {
                    Post postAux = p as Post;
                    posts.Add(postAux);
                }
            }
            return posts;
        }

        public List<Comentario> GetComentarios()
        {
            //Obtener la lista de comentarios del sistema.
            List<Comentario> comentarios = new List<Comentario>();
            foreach (Publicacion p in _publicaciones)
            {
                //Verificamos si la publicaciones es un Comentario
                if (p != null && p.GetTipo() == "Comentario")
                {
                    Comentario comentarioAux = p as Comentario;
                    comentarios.Add(comentarioAux);
                }
            }
            return comentarios;
        }

        //public List<Miembro> GetAmigosMiembro(Miembro m) //EVALUAR SI ES NECESARIO A FUTURO
        //{
        //    //TODO miembro en sistema
        //    List<Miembro> amigos = new List<Miembro>();
        //    amigos = m.GetAmigosMiembro();
        //    return amigos;
        //}

        //Metodo provado para obtener un Miembro por su Email
        public Miembro GetMiembroPorEmail(string Email)
        {
            foreach (Miembro usuario in GetMiembros())
            {
                //Buscar un Miembro por su Email.
                if (usuario.Email.Equals(Email))
                {
                    return usuario;
                }
            }
            //Lanzamos una excepcion si el Email no esta en el sistema
            throw new Exception($"El correo electronico '{Email}' no esta en el sistema");
        }

        public Usuario GetUsuarioPorEmail(string Email)
        {
            foreach (Usuario usuario in _usuarios)
            {
                //Buscar un Miembro por su Email.
                if (usuario.Email.Equals(Email))
                {
                    return usuario;
                }
            }
            //Lanzamos una excepcion si el Email no esta en el sistema
            throw new Exception($"El correo electronico '{Email}' no esta en el sistema");
        }

        public Usuario GetUsuarioLogueado(int? idUsuarioLogueado)
        {
            foreach (Usuario u in _usuarios)
            {
                if (u.Id.Equals(idUsuarioLogueado))
                {
                    return u;
                }
            }
            return null;
        }

        //Listamos los posts que estén habilitados para un usuario del que conocemos el id
        public List<Post> GetPostsHabilitados(int? idLogueado)
        {
            List<Post> listaRet = new List<Post>();
            if (idLogueado != null)
            {
                foreach (Post p in GetPosts())
                {
                    if (!p.Privado || p.Autor.Id.Equals(idLogueado) || p.Autor.ListaAmigos.Contains(GetUsuarioLogueado(idLogueado)))
                    {
                        if (!p.Censurado)
                        {
                            listaRet.Add(p);
                        }
                    }
                }
            }
            return listaRet;
        }

        // Buscamos un post en nuestros Posts de la lista de publicaciones por su id
        public Post GetPostPorId(int idPost)
        {
            foreach (Post p in GetPosts())
            {
                if (p.Id.Equals(idPost))
                {
                    return p;
                }
            }
            return null;
        }

        //Listamos los comentarios de un post del que conocemos el id, que estén habilitados para un usuario del que conocemos el id
        public List<Comentario> GetComentariosHabilitados(int idPost, int? idLogueado)
        {
            List<Comentario> listaRet = new List<Comentario>();
            if (idLogueado != null)
            {
                Post p = GetPostPorId(idPost);
                Usuario u = GetUsuarioLogueado(idLogueado);
                Miembro m = GetMiembroPorEmail(u.Email);
                foreach (Comentario c in p.GetComentarios())
                {
                    if (!c.Privado || c.Autor.Id.Equals(idLogueado) || c.Autor.ListaAmigos.Contains(m))
                    {
                        listaRet.Add(c);
                    }
                }
            }
            return listaRet;
        }

        public Publicacion GetPublicacionPorId(int idPublicacion)
        {
            foreach (Publicacion p in _publicaciones)
            {
                if (p.Id.Equals(idPublicacion))
                {
                    return p;
                }
            }
            return null;
        }

        public List<Publicacion> GetPublicacionesHabilitadas(int? idLogueado)
        {
            List<Publicacion> listaRet = new List<Publicacion>();
            if (idLogueado != null)
            {
                Usuario u = GetUsuarioLogueado(idLogueado);
                Miembro m = GetMiembroPorEmail(u.Email);
                foreach (Publicacion p in GetPublicaciones())
                {
                    if (!p.Privado || p.Autor.Id.Equals(idLogueado) || p.Autor.ListaAmigos.Contains(m))
                    {
                        if (p.GetTipo() == "Post")
                        {
                            Post po = (Post)p;
                            if (!po.Censurado)
                            {
                                listaRet.Add(po);
                            }
                        }
                        else
                        {
                            listaRet.Add(p);
                        }
                    }
                }
            }
            return listaRet;
        }

        public List<Publicacion> GetPublicacionesFiltradas(string buscar, int numero, int? idLogueado)
        {
            List<Publicacion> listaHabilitada = GetPublicacionesHabilitadas(idLogueado);
            List<Publicacion> listaRet = new List<Publicacion>();
            foreach (Publicacion p in listaHabilitada)
            {
                if (p.CalcularValorAceptacion() > numero)
                {
                    if (p.Texto.IndexOf(buscar, StringComparison.OrdinalIgnoreCase) >= 0 || p.Titulo.IndexOf(buscar, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        listaRet.Add(p);
                    }
                }
            }
            return listaRet;
        }

        public List<Invitacion> GetInvitacionesMiembro(int? idUsuarioLogueado)
        {
            List<Invitacion> listaRet = new List<Invitacion>();
            foreach (Invitacion i in _invitaciones)
            {
                if (i.MiembroSolicitado.Id.Equals(idUsuarioLogueado) && i.Estado == EstadoInvitacion.Pendiente_Aprobacion)
                {
                    listaRet.Add(i);
                }
            }
            return listaRet;
        }

        public Invitacion GetInvitacionPorID(int idInvitacion)
        {
            foreach (Invitacion i in _invitaciones)
            {
                if (i.Id.Equals(idInvitacion))
                {
                    return i;
                }
            }
            return null;
        }

        public List<Miembro> GetMiembrosHabilitados(int? idUsuarioLogueado)
        {
            List<Miembro> listaRet = new List<Miembro>();
            Usuario u = GetUsuarioLogueado(idUsuarioLogueado);
            Miembro miembro = (Miembro)u;
            foreach (Miembro m in GetMiembros())
            {
                if (!m.Bloqueado && !miembro.ListaAmigos.Contains(m) && m.Id != miembro.Id)
                {
                    if (!MiembroTieneInvitacionPendiente(miembro, m))
                    {
                        listaRet.Add(m);
                    }
                }
            }
            return listaRet;
        }

        #endregion

        #region IUConsola 

        //Aqui estan los metodos que llamamos en program.

        // Método que lista las publicaciones de un usuario dado su correo electrónico
        public List<Publicacion> ListarPublicacionesUsuario(string Email)
        {
            try
            {
                //Verificamos si el Email esta vacio.
                if (string.IsNullOrEmpty(Email))
                {
                    throw new Exception("El mail no puede ser vacio");
                }

                //Verificamos si el Email esta en la lista de usuarios del sistema.
                bool correoEncontrado = false;
                foreach (Usuario u in _usuarios)
                {
                    if (u.Email == Email)
                    {
                        correoEncontrado = true;
                        break;

                    }

                }
                //Si el Email esta en el sistema, obtenemos las publicaciones asociadas al usuario.
                if (correoEncontrado)
                {
                    List<Publicacion> listaPublicaciones = new List<Publicacion>();
                    foreach (Publicacion pub in _publicaciones)
                    {
                        if (pub.Autor.Email == Email)
                        {

                            listaPublicaciones.Add(pub);
                        }

                    }
                    return listaPublicaciones;
                }
                else
                {
                    throw new Exception("El correo no se encuentra en nuestra base de datos");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        // Método que lista los posts que contienen comentarios de un usuario
        public List<Post> ListarPostConComentarioUsuario(string Email)
        {
            // verificar de hacer metodo extra 'existe usuario'

            try
            {
                //Verificamos si el Email esta vacio.
                if (string.IsNullOrEmpty(Email))
                {
                    throw new Exception("El mail no puede ser vacio");
                }
                //Obtenemos el usuario con el Email proporcionado.
                List<Post> listaPost = new List<Post>();
                Miembro usuario = GetMiembroPorEmail(Email);

                //Recorremos las publicaciones y agregamos los post que tienen comentarios del usuario.
                foreach (Publicacion pubCaso2 in _publicaciones)
                {
                    if (pubCaso2.GetTipo() == "Post")
                    {
                        Post postAux = pubCaso2 as Post;
                        if (UsuarioTieneComentario(usuario, postAux.GetComentarios()))
                        {
                            listaPost.Add(postAux);
                        }
                    }
                }

                return listaPost;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                throw;
            }
        }

        // Método que verifica si un usuario tiene al menos un comentario en una lista de comentarios
        public bool UsuarioTieneComentario(Usuario usuario, List<Comentario> lista)
        {

            foreach (Comentario c in lista)
            {
                if (c.Autor == usuario)
                {
                    return true;
                }
            }
            return false;

        }

        // Método que lista los posts que fueron publicados entre dos fechas
        public List<Post> ListarPostEntreFechas(string fecha1, string fecha2)
        {
            List<Post> listaPost = new List<Post>();

            //Convertimos las fechas al formato DateTime.
            DateTime fecha1Form = ConvertirStringADateTime(fecha1);
            DateTime fecha2Form = ConvertirStringADateTime(fecha2);

            //Verificamos la diferencia entre fechas para determinar la fecha mas reciente
            TimeSpan diferencia = fecha1Form - fecha2Form;
            DateTime mayor = fecha1Form;

            if (diferencia.TotalDays < 0)
            {
                mayor = fecha2Form;
                fecha2Form = fecha1Form;
            }

            //Filtramos los posts que fueron publicados entre las fechas proporcionadas
            foreach (Publicacion p in _publicaciones)
            {
                if (p.GetTipo() == "Post")
                {
                    Post postAux = p as Post;
                    if (postAux.FechaPublicacion >= fecha2Form && postAux.FechaPublicacion <= mayor)
                    {
                        listaPost.Add(postAux);
                    }
                }
            }
            //Ordenamos la lista de forma descendente
            listaPost.Sort();
            //listaPost.Reverse();
            return listaPost;
        }

        // Método que lista los miembros con más publicaciones.
        public List<Miembro> MiembroConMasPublicaciones()
        {
            List<Miembro> listaRet = new List<Miembro>();
            int mayor = -1;
            //Iteramos sobre todas las publicaciones y contamos las publicaciones de cada Miembro llamando un metodo.
            foreach (Publicacion p in GetPublicaciones())
            {
                int aux = ContarPublicacionesDeMiembro(p.Autor);
                if (aux > mayor)
                {
                    listaRet.Clear();
                    listaRet.Add(p.Autor);
                    mayor = aux;
                }
                else if (aux == mayor)
                {
                    //Si el usuario aun no está en la lista lo agrega
                    if (!listaRet.Contains(p.Autor))
                    {
                        listaRet.Add(p.Autor);
                    }
                }
            }

            return listaRet;
        }

        // Método que cuenta las publicaciones de un miembro dado
        public int ContarPublicacionesDeMiembro(Miembro m)
        {
            int cantidad = 0;

            // Contamos las publicaciones asociadas a un miembro específico
            foreach (Publicacion p in GetPublicaciones())
            {
                if (p.Autor == m)
                {
                    cantidad++;
                }
            }
            return cantidad;
        }

        public DateTime ConvertirStringADateTime(string fecha)
        {
            try
            {
                if (ValidarFormatoFecha(fecha))
                {
                    DateTime fechaForm = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    return fechaForm;
                }
                else
                {
                    throw new Exception("La fecha debe tener formato DD/MM/AAAA");
                }
            }
            catch (Exception e)
            {
                throw new Exception("La fecha debe tener dormato DD/MM/AAAA");
            }
        }

        public bool ValidarFormatoFecha(string fecha)
        {
            return DateTime.TryParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaForm);
        }



        #endregion

        #region IUWebApp

        public bool MiembroTieneInvitacionPendiente(Miembro solicitante, Miembro solicitado)
        {
            foreach (Invitacion i in _invitaciones)
            {
                if (i.MiembroSolicitado.Id.Equals(solicitado.Id) && i.MiembroSolicitante.Id.Equals(solicitante.Id))
                {
                    if (i.Estado == EstadoInvitacion.Pendiente_Aprobacion)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion


        
    }
}
