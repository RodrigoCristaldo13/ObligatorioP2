using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    // La clase abstracta Publicacion implementa las interfaces IValidable e IComparable<Post>
    public abstract class Publicacion : IValidable, IComparable<Post>
    {
        public int Id { get; set; }

        public static int UltimoId { get; set; } = 1;

        public Miembro Autor { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string Texto { get; set; } // Tomamos como texto al contenido de las publicaciones

        public string Titulo { get; set; }

        public bool Privado { get; set; }

        //Protejemos la lista, haciéndola privada y sin set
        private List<Reaccion> _reacciones { get; } = new List<Reaccion>();

        
        //Creacion de constructores
        public Publicacion()
        {
            Id = UltimoId;
            UltimoId++;
        }

        public Publicacion(Miembro autor, string titulo, string texto, bool privado, List<Reaccion> reacciones)
        {
            Id = UltimoId;
            UltimoId++;
            Autor = autor;
            FechaPublicacion = DateTime.Now;
            Titulo = titulo;
            Privado = privado;
            _reacciones = reacciones;
            Texto = texto;
        }


        // Método para agregar una reacción a la publicación.
        public void AgregarReaccion(Reaccion r)
        { 
            // Evaluar si es necesaria
            // Evaluamos si el usuario ya reaccionó y actualizamos la reacción existente o agregamos una nueva
            bool usuarioYaReacciono = false;

            foreach (Reaccion reaccionExistente in _reacciones)
            {
                if (reaccionExistente.Autor == r.Autor)
                {
                    usuarioYaReacciono = true;
                    if (reaccionExistente.Tipo.Equals(r.Tipo))
                    {
                        _reacciones.Remove(reaccionExistente);
                    }
                    else
                    {
                        reaccionExistente.Tipo = r.Tipo;
                    }
                    break;
                }
            }
            if (!usuarioYaReacciono)
            {
                _reacciones.Add(r);
            }
        }

        public List<Reaccion> GetReacciones()
        {
            return _reacciones;
        }

        public int GetReaccionesTipo(string tipo)
        {
            int cantidadReacciones = 0;
            foreach (Reaccion r in _reacciones)
            {
                if (r.ToString() == tipo)
                {
                    cantidadReacciones++;
                }
            }
            return cantidadReacciones;
        }

        #region VALIDACIONES

        // Implementación del método Validar de la interfaz IValidable
        public virtual void Validar()
        {
            // Llamamos a los metodos privados de validación
            ValidarTexto();
            ValidarTitulo();
        }

        // Método privado para validar que el texto no esté vacío
        private void ValidarTexto()
        {
            //Validamos texto no vacío
            if (string.IsNullOrEmpty(Texto)) 
            {
                throw new Exception("El texto no debe estar vacio");
            }
        }

        private void ValidarTitulo()
        {
            //Verificamos si el titulo no es vacio
            if (string.IsNullOrEmpty(Titulo)) //Validamos titulo no vacío
            {
                throw new Exception("El titulo no debe estar vacio");
            }
            //Validamos que el titulo tenga al menos 3 caracteres.
            if (Titulo.Length < 3) 
            {
                throw new Exception("El titulo debe tener al menos 3 caracteres");
            }
        }

        #endregion

        #region ManejoDeString

        // Implementación del método ToString para generar un mensaje personalizado.
        public override string ToString()
        {
            return $"Tipo: {GetTipo()}; Id : {Id}, Fecha de Publicacion :  {FechaPublicacion}, Titulo : {Titulo}, Texto: {CortarTexto(Texto)}..."; // Redefinimos toString para adaptarlo a nuestras necesidades
        }

        // Metodo que corta un texto para que no supere los 50 caracteres, si el texto dado tiene un largo menor a 50 caracteres no necesita ser cortado
        public string CortarTexto(string txt) 
        {
            if (txt.Length > 50)
            {
                string nuevoTexto = "";
                for (int i = 0; i <= txt.Length; i++)
                {
                    if (i < 50)
                    {
                        nuevoTexto += txt[i];
                    }
                    else
                    {
                        break;
                    }
                }
                return nuevoTexto;
            }
            else
            {
                return txt;
            }
        }

        #endregion
        // Métodos abstractos para ser implementados en las clases derivadas
        public abstract int CalcularValorAceptacion(); // Solo firma

        public abstract string GetTipo(); // Solo firma


        // Metodo de la interfaz IComparable<Post> para establecer un criterio de ordenamiento.
        public int CompareTo(Post other)
        {
            if (Titulo.CompareTo(other.Titulo) > 0)
            {
                return -1;
            }
            else if (Titulo.CompareTo(other.Titulo) < 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public string TiempoTranscurrido() //Calcula el tiempo que pasó desde la publicacion a la hora actual
        {
            TimeSpan diferencia = DateTime.Now - FechaPublicacion;
            double segundos = Math.Round(diferencia.TotalSeconds);
            string ret = segundos.ToString();
            if(segundos > 59)
            {
                double minutos = Math.Round(diferencia.TotalMinutes);
                if (minutos >= 1 && minutos <= 59)
                {
                    ret = minutos.ToString();
                    return $"Hace {ret} minutos";
                }
                else if (minutos > 59 && minutos < 1440)
                {
                    double horas = Math.Round(diferencia.TotalHours);
                    ret = horas.ToString();
                    return $"Hace {ret} horas";
                }
                else if (minutos >= 1440)
                {
                    double dias = Math.Round(diferencia.TotalDays);
                    ret = dias.ToString();
                    return $"Hace {ret} dias";
                }
            }
            return $"Hace {ret} segundos";
        }

    }
}
