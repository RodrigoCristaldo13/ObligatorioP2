using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    // La clase Post hereda de Publicacion e implementa la interfaz IValidable
    public class Post : Publicacion, IValidable
    {
        public string Imagen { get; set; }
        private List<Comentario> _comentarios { get; } = new List<Comentario>();
        public bool Censurado { get; set; }

        //Creacion de constructores
        public Post()
        {

        }

        public Post(string imagen, List<Comentario> comentarios, bool censurado, Miembro autor, string titulo, string texto, bool privado, List<Reaccion> reacciones) : base(autor, titulo, texto, privado, reacciones)
        {
            Imagen = imagen;
            Censurado = censurado;
            _comentarios = comentarios;

        }


        //Generamos un constructor para poder cargar post con fechas anteriores y así poder darle uso a la 3ra funcionalidad del Menú usuario
        public Post(string imagen, List<Comentario> comentarios, bool censurado, Miembro autor, DateTime fecha, string titulo, string texto, bool privado, List<Reaccion> reacciones) : base(autor, titulo, texto, privado, reacciones)
        {
            Imagen = imagen;
            Censurado = censurado;
            _comentarios = comentarios;
            FechaPublicacion = fecha;

        }

        //Metodo para agregar comentarios a la lista de comentarios si es que se cumplen las validaciones impuestas, por lo contrario se lanzarán las excepciones correspondientes 
        public void AltaComentario(Comentario c)
        {
            try
            {
                if (c != null)
                {
                    c.Validar();
                    _comentarios.Add(c);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Metodo para obtener la lista de comentarios de manera segura, sin acceder directamente a la lista privada
        public List<Comentario> GetComentarios()
        {
            return _comentarios;
        }
        

        // Implementación del método abstracto GetTipo de la clase base
        public override string GetTipo()
        {
            return "Post";
        }

        #region VALIDACIONES 

        // Implementación del método abstracto Validar de la interfaz IValidable
        public override void Validar()
        // Utilizamos validaciones del padre (Publicacion) y ademas validamos extension 
        {
            base.Validar();
            ValidarExtension();
        }

        //Validamos que el texto de la imagen no sea vacío y la extencion de la imagen sea la correcta
        private void ValidarExtension()

        {
            if (string.IsNullOrEmpty(Imagen))
            {
                throw new Exception("La imagen del post no puede estar vacia");
            }

            //Usamos la opción de hacerlo insensible a mayusculas y minusculas con ".OrdinalIgnoreCase"

            if (!Imagen.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) && !Imagen.EndsWith(".png", StringComparison.OrdinalIgnoreCase))

            {
                // Solo lanza la excepcion en caso que la extencion no termine en las dos formas
                throw new Exception("La extension de la imagen debe ser (.jpg) o (.png)");
            }

        }

        #endregion

        #region UtilizableAFuturo

        // El metodo lo utilizaremos a futuro
        public override int CalcularValorAceptacion()
        {
            int likes = GetReaccionesTipo("like");
            int dislikes = GetReaccionesTipo("dislike");
            int retorno = (likes * 5) + (dislikes * -2);
            if (!Privado)
            {
                retorno += 10;
            }
            return retorno;
        }

        //public void MostrarPostPrivado(List<Miembro> listaAmigo)  //Lo guardamos para el futuro
        //{
        //    if (Privado && !listaAmigo.Contains(Autor))
        //    {
        //        throw new Exception("No tiene acceso al post privado");
        //    }
        //}

        #endregion


    }
}