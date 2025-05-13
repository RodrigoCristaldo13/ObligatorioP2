using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    // En esta clase utilizamos la interface IValidable
    public class Invitacion : IValidable
    {
        public int Id { get; set; }
        public static int UltimoId { get; set; } = 1;

        public Miembro MiembroSolicitante { get; set; }
        public Miembro MiembroSolicitado { get; set; }
        public EstadoInvitacion Estado { get; set; }
        public DateTime FechaSolicitud { get; set; }


        //Creamos los contructores
        public Invitacion()
        {
            Id = UltimoId;
            UltimoId++;
        }

        public Invitacion(Miembro miembroSolicitante, Miembro miembroSolicitado, EstadoInvitacion estado)
        {
            Id = UltimoId;
            UltimoId++;
            MiembroSolicitante = miembroSolicitante;
            MiembroSolicitado = miembroSolicitado;
            Estado = estado;
            FechaSolicitud = DateTime.Now; //La fecha de la invitacion siempre va a ser su fecha de creación
        }

        #region VALIDACIONES

        // Implementación del método de la interfaz IValidable
        public void Validar()
        {
            ValidarMiembroNoVacio();
            ValidarMiembrosNoBloqueados();
        }

        //posibilidad de separar validar miembro bloqueado por solicitante y solicitado o dejarlos en un metodo

        //Metodo privado para validar que los miembros no esten bloqueados
        private void ValidarMiembrosNoBloqueados()
        {
            if (MiembroSolicitante.Bloqueado || MiembroSolicitado.Bloqueado)
            {
                throw new Exception("Los miembros no pueden estar bloqueados por el administrador para enviar o recibir invitaciones");
            }
        }

        // Método privado para validar que los miembros no sean nulos
        private void ValidarMiembroNoVacio()
        {
            if (MiembroSolicitante == null || MiembroSolicitado == null)
            {
                throw new Exception($"Ambos miembros deben existir en el sistema");
            }
        }

        #endregion

        //Metodo ToString para mostrar un mensaje personalizado
        public override string ToString()
        {
            return "Invitación";
        }

        public string TiempoTranscurrido() //Calcula el tiempo que pasó desde la invitacion a la hora actual
        {
            TimeSpan diferencia = DateTime.Now - FechaSolicitud;
            double segundos = Math.Round(diferencia.TotalSeconds);
            string ret = segundos.ToString();
            if (segundos > 59)
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
