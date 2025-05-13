using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    // La clase Usuario implementa la interfaz IValidable.
    public abstract class Usuario : IValidable
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; } = 1;
        public string Email { get; set; }
        public string Contrasenia { get; set; }

        //Creamos los constructores para usuario
        public Usuario()
        {
            Id = UltimoId;
            UltimoId++;
        }

        public Usuario(string email, string contrasenia)
        {
            Id = UltimoId;
            UltimoId++;
            Email = email;
            Contrasenia = contrasenia;
        }

        //Validamos los metodos privados
        public virtual void Validar() 
        {
            ValidarContrasenia();
            ValidarEmail();
        }

        
        private void ValidarContrasenia()
        {
            //Verificamos que la contrasenia no sea nula.
            if (string.IsNullOrEmpty(Contrasenia))
            {
                throw new Exception("La contraseña no puede ser vacia");
            }
            //Segunda verificacion, la contrasenia no puede ser menor que 5
            if (Contrasenia.Length < 5)
            {
                throw new Exception("La contraseña debe tener al menos 5 caracteres");
            }
        }

        private void ValidarEmail()
        {
            //Verificamos si el Email es nulo.
            if (string.IsNullOrEmpty(Email))
            {
                throw new Exception("El mail no debe estar vacio");
            }
        }

        // Metodo firmado, aplicamos su funcionalidad en los hijos (Administrador y Miembro)
        public abstract string GetTipo(); 

    }
}

