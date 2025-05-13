using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    //La clase Comentario hereda de Publicacion.
    public class Comentario : Publicacion
    {

        //Creacion de contructores
        public Comentario()
        {
            
        }

        public Comentario(Miembro autor, string titulo, string texto, bool privado, List<Reaccion> reacciones) : base(autor, titulo, texto, privado, reacciones)
        {
            
        }

        // Implementación del método abstracto Validar de la clase base
        public override void Validar()  
            
        {
            // Utilizamos las mismas validaciones del Padre (Publicacion)
            base.Validar();
        }

        // Implementación del método abstracto GetTipo de la clase base
        public override string GetTipo()
        {
            return "Comentario";
        }

        // Implementación del método abstracto CalcularValorAceptacion de la clase base
        public override int CalcularValorAceptacion()
        {
            int likes = GetReaccionesTipo("like");
            int dislikes = GetReaccionesTipo("dislike");
            int retorno = (likes * 5) + (dislikes * -2);
            
            return retorno;
        }

    }
}
