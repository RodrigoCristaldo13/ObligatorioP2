using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Reaccion
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; } = 1;
        public Tipo Tipo { get; set; }
        public Miembro Autor { get; set; }

        //Creamos los constructores
        public Reaccion()
        {
            Id = UltimoId;
            UltimoId++;
        }

        public Reaccion(Tipo tipo, Miembro autor)
        {
            Id = UltimoId;
            UltimoId++;
            Tipo = tipo;
            Autor = autor;
        }

        //Sobreescribimos el metodo ToString para mostrar un mensaje personalizado
        public override string ToString()
        {
            return $"{Tipo}";
        }
    }
}
