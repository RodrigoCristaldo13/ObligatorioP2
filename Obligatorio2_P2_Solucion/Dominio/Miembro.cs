using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    // La clase Miembro hereda de la clase base Usuario e implementa la interfaz IValidable
    public class Miembro : Usuario, IValidable, IComparable<Miembro>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Miembro> ListaAmigos { get; } = new List<Miembro>();
        public bool Bloqueado { get; set; }

        //Creamos los constructores.
        public Miembro()
        {

        }

        public Miembro(string nombre, string apellido, DateTime fechaNacimiento, bool bloqueado, string email, string contrasenia) : base(email, contrasenia)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            ListaAmigos = new List<Miembro>(); //Creamos una nueva lista de amigos cuando se crea el Miembro
            Bloqueado = bloqueado;
        }

        #region VALIDACIONES

        //Implementacion del metodo validar de la interfaz Ivalidable
        public override void Validar()
        {
            base.Validar();// Validacion de la clase base Usuario
            ValidarNombreApellido();
        }

        // Método privado para validar que el nombre y el apellido no estén vacíos
        private void ValidarNombreApellido()
        {
            //Verificamos si es nulo el nombre.
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede estar vacio");
            }
            //Verificamos si es nulo el apellido.
            if (string.IsNullOrEmpty(Apellido))
            {
                throw new Exception("El apellido no puede estar vacio");
            }
        }

        #endregion

        //Metodo para agregar un amigo a la lista de amigos del miembro
        public void AgregarAmigo(Miembro miembroAmigo)
        {
            //Verificamos si el miembro no esta ya en la lista de amigos
            if (!ListaAmigos.Contains(miembroAmigo))
            {
                //Verificamos si ninguno de los miembros esta bloqueado para agregar un amigo.
                if (!Bloqueado && !miembroAmigo.Bloqueado)
                {
                    ListaAmigos.Add(miembroAmigo);
                }
                else if(!Bloqueado)
                {
                    //Lanzamos una excepcion si uno de los miembros esta bloqueado.
                    throw new Exception("No se puede aceptar la solicitud, el usuario seleccionado está bloqueado");
                }
                else if (!miembroAmigo.Bloqueado)
                {
                    //Lanzamos una excepcion si ambos miembros están bloqueados.
                    throw new Exception("No se puede aceptar la solicitud, usted está bloqueado");
                }
                else
                {
                    //Lanzamos una excepcion si ambos miembros están bloqueados.
                    throw new Exception("No se puede aceptar la solicitud, los miembros no pueden estar bloqueados");
                }
            }
            else
            {
                //Lanzamos una excepcion si el miembro ya es amigo del actual.
                throw new Exception($"{Nombre} ya es amigo de {miembroAmigo.Nombre}");
            }
        }

        //Metodo que devuelve el tipo de usuario('Miembro').
        public override string GetTipo()
        {
            return "Miembro";
        }

        // Método que sobrescribe el método ToString con un mensaje personalizado.
        public override string ToString()
        {
            return $"Nombre: {Nombre}, Apellido: {Apellido}, Fecha Nacimiento: {FechaNacimiento}, Email: {Email}.";
        }

        public int CompareTo(Miembro other)
        {
            string nombreCompleto = Apellido + " " + Nombre;
            string otherNombreCompleto = other.Apellido + " " + other.Nombre;
            return nombreCompleto.CompareTo(otherNombreCompleto);
        }

        #region UtilizableAFuturo

        //public List<Miembro> GetAmigosMiembro()

        //EVALUAR SI ES NECESARIO
        //{
        //    return ListaAmigos;
        //}

        public void AceptarSolicitud(Invitacion i)
        {
            if (i.Estado == EstadoInvitacion.Pendiente_Aprobacion)
            {
                i.Estado = EstadoInvitacion.Aprobada;
                AgregarAmigo(i.MiembroSolicitante);
                i.MiembroSolicitante.AgregarAmigo(this); // i.MiembroSolicitado es una alternativa al this
            }
        }

        public void RechazarSolicitud(Invitacion i)
        {
            if (i.Estado == EstadoInvitacion.Pendiente_Aprobacion)
            {
                i.Estado = EstadoInvitacion.Rechazada;
            }
        }


        #endregion

    }
}
