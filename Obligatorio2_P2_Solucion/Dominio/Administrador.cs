using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    //La clase Administrador hereda de la clase Padre Usuario
    public class Administrador : Usuario
    {
        //Constructores de Administrador.
        public Administrador()
        {

        }

        public Administrador(string email, string contrasenia) : base(email, contrasenia)
        {

        }

        //Metodo para bloquear a un miembro.
        public void BloquearUsuario(Miembro m)
        {
            //Verificamos si no es nulo y lo marcamos como bloqueado.
            if (m != null)
            {
                m.Bloqueado = true;
            }
        }

        //Metodo para censurar un post
        public void CensurarPost(Post p)
        {
            //Verificamos si el post no es nulo y lo marcamos como censurado.
            if (p != null)
            {
                p.Censurado = true;
            }
        }

        //Metodo que devuelve el tipo de usuario (Administrador)
        public override string GetTipo()
        {
            return "Administrador";
        }

    }
}
