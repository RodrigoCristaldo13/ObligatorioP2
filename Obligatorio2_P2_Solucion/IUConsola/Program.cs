using Dominio;
using System.Globalization;

namespace Obl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sistema s = Sistema.Instancia();

            #region MENU

            int opcion = -1;
            while (opcion != 0)
            {
                Console.Clear();
                Console.WriteLine("Bienvenido a tu SOCIAL NETWORK favorita");
                Console.WriteLine("Seleccione una opción");
                Console.WriteLine("1 - Alta Miembro");
                Console.WriteLine("2 - Mostrar Publicaciones de un Usuario");
                Console.WriteLine("3 - Mostrar Posts que contengan comentarios de un Usuario");
                Console.WriteLine("4 - Mostrar Posts entre fechas");
                Console.WriteLine("5 - Mostrar Miembro/s con más Publicaciones");
                Console.WriteLine("0 - Salir");

                try
                {
                    string opcionString = Console.ReadLine(); // opcion auxiliar para hacer validacion de string
                    string opciones = "1 2 3 4 5 0";
                    while (string.IsNullOrWhiteSpace(opcionString) || !opciones.Contains(opcionString)) // Validamos que la opcion no sea un string vacio y sea una de nuestras opciones disponibles
                    {
                        Console.WriteLine("Por favor, Ingrese una opción correcta (Opciones del 0 a 5) ");
                        opcionString = Console.ReadLine();
                    }
                    if (int.TryParse(opcionString, out int opcionNueva))
                    {
                        opcion = opcionNueva;
                        //opcion = Int32.Parse(Console.ReadLine());
                    }
                    else
                    {
                        throw new Exception("Ha ingresado una opción invalida");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Ha seleccionado la opcion 1, para ser dado de Alta en nuestra Red Social ingrese sus datos a continuación");
                        RealizarNuevaAltaUsuario(s);

                        Console.WriteLine("Presione una tecla para regresar al menú");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ha seleccionado la opcion 2, Se mostrarán las publicaciones del usuario");
                        MostrarLasPublicacionesUsuario(s);

                        Console.WriteLine("Presione una tecla para regresar al menú");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Ha seleccionado la opcion 3, Se mostrarán los Post existentes que tengan comentarios del usuario ingresado");
                        MostrarPostConComentariosDeUsuario(s);

                        Console.WriteLine("Presione una tecla para regresar al menú");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Ha seleccionado la opcion 4, Se mostrarán los Post existentes entre las fechas ingresadas");
                        MostrarPostEntreFechas(s);

                        Console.WriteLine("Presione una tecla para regresar al menú");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("Ha seleccionado la opcion 5, Se mostrarán el o los miembros con más publicaciones de cualquier tipo");
                        ListarMiembrosConMasPublicaciones(s);

                        Console.WriteLine("Presione una tecla para regresar al menú");
                        Console.ReadKey();
                        break;
                }
            }
            #endregion

            #region MetodosParaCadaCaseDelSwitch

            //Metodo para case 1
            static void RealizarNuevaAltaUsuario(Sistema s)
            {
                try
                {
                    //Solicitamos datos al usuario para darlo de alta y pedimos reingreso de datos en caso que sean vacios
                    Console.WriteLine("Ingrese su nombre");
                    string nombreUsuario = Console.ReadLine();

                    while (String.IsNullOrWhiteSpace(nombreUsuario))
                    {
                        Console.WriteLine("Ingrese un nombre no vacío");
                        nombreUsuario = Console.ReadLine();
                    }

                    Console.WriteLine("Ingrese su apellido");
                    string apellidoUsuario = Console.ReadLine();

                    while (String.IsNullOrWhiteSpace(apellidoUsuario))
                    {
                        Console.WriteLine("Ingrese un apellido no vacío");
                        apellidoUsuario = Console.ReadLine();
                    }

                    Console.WriteLine("Ingrese su fecha de nacimiento (DD/MM/AAAA)");
                    string fechaNacimiento = Console.ReadLine();

                    while (string.IsNullOrWhiteSpace(fechaNacimiento) || !s.ValidarFormatoFecha(fechaNacimiento))
                    {
                        Console.WriteLine("Ingrese una fecha real (con formato DD/MM/AAAA) que no sea vacía");
                        fechaNacimiento = Console.ReadLine();
                    }

                    Console.WriteLine("Ingrese su Email");
                    string emailUsuario1 = Console.ReadLine();

                    while (String.IsNullOrWhiteSpace(emailUsuario1) || !emailUsuario1.Contains('@'))
                    {
                        Console.WriteLine("Ingrese un Email no vacío y que contenga @");
                        emailUsuario1 = Console.ReadLine();
                    }

                    Console.WriteLine("Ingrese su nueva contraseña");
                    string passUsuario = Console.ReadLine();

                    while (passUsuario.Length < 5)
                    {
                        Console.WriteLine("Ingrese una contraseña con al menos 5 caracteres");
                        passUsuario = Console.ReadLine();
                    }

                    //Llamamos a la funcion "NuevoMiembro" que va a crear al nuevo Miembro luego de validar nuevamente los datos
                    s.NuevoMiembro(nombreUsuario, apellidoUsuario, fechaNacimiento, emailUsuario1, passUsuario);
                    //En caso de acierto mostramos un mensaje al usuario con el nombre y apellido del miembro creado.
                    Console.WriteLine($"Se agregó correctamente a {nombreUsuario} {apellidoUsuario} ");
                    Console.WriteLine("____________");
                }
                catch (Exception e)
                {
                    //En caso de error mostramos un mensaje, especificando la causa.
                    Console.WriteLine(e.Message);
                }
            }

            //Metodo para case 2
            static void MostrarLasPublicacionesUsuario(Sistema s)
            {
                try
                {
                    //Pedimos email del usuario para mostrar sus publicaciones
                    Console.WriteLine("Ingrese el Email del usuario:");
                    string emailUsuario = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(emailUsuario) || !emailUsuario.Contains('@')) // Pedimos reingreso de email si no se cumple el formato
                    {
                        Console.WriteLine("Ingrese un Email no vacío y que contenga @");
                        emailUsuario = Console.ReadLine();
                    }
                    Console.WriteLine("");

                    //Obtenemos la lista de publicaciones del usuario mediante un metodo.
                    List<Publicacion> listaPublicaciones = s.ListarPublicacionesUsuario(emailUsuario);

                    if (listaPublicaciones.Count == 0)
                    {
                        Console.WriteLine("No hay publicaciones asociadas al usuario");
                    }
                    else
                    {
                        //Mostramos las publicaciones
                        Console.WriteLine("Estas son las publicaciones que realizó el usuario: ");
                        foreach (Publicacion p in listaPublicaciones)
                        {
                            Console.WriteLine("__________");
                            Console.WriteLine("");
                            Console.WriteLine($"{p.GetTipo()}: {p.Titulo}");
                        }
                    }
                }
                catch (Exception e)
                {
                    //Mensaje de error
                    Console.WriteLine(e.Message);
                }
            }

            //Metodo para case 3

            //retornar si no hay null
            static void MostrarPostConComentariosDeUsuario(Sistema s)
            {
                try
                {
                    //Solicitamos email del usuario para mostrar posts con comentarios
                    Console.WriteLine("Ingrese el email del usuario:");
                    string emailUsuarioCaso2 = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(emailUsuarioCaso2) || !emailUsuarioCaso2.Contains('@')) // Pedimos reingreso de email si no se cumple el formato
                    {
                        Console.WriteLine("Ingrese un Email no vacío y que contenga @");
                        emailUsuarioCaso2 = Console.ReadLine();
                    }
                    Console.WriteLine("");

                    List<Post> listaPostCaso2 = s.ListarPostConComentarioUsuario(emailUsuarioCaso2);
                    //Especificamos distintos mensajes para los distintos casos:
                    if (listaPostCaso2.Count == 0)
                    {
                        //Si el correo del usuario no tiene ningun comentario en posts mostramos mensaje.
                        Console.WriteLine("No existe ningun post con comentarios del usuario");
                    }
                    else
                    {
                        //Si el correo del usuario es valido y tiene comentarios en posts mostramos las posts en los que realizó comentarios.
                        Console.WriteLine("Estos son las post en los que el usuario realizó comentarios: ");

                        foreach (Post p in listaPostCaso2)
                        {

                            Console.WriteLine(p.Titulo);
                            Console.WriteLine("_______________");

                        }
                    }
                }
                catch (Exception e)
                {
                    //Capturamos y mostramos al usuario un mensaje de error especifico.
                    Console.WriteLine(e.Message);
                }
            }

            //Metodo para case 4
            static void MostrarPostEntreFechas(Sistema s)
            {
                try
                {
                    //Solicitamos 2 fechas al usuario en un formato especifico 'dd/MM/yyyy'.

                    Console.WriteLine("Ingrese la primera fecha (formato: DD/MM/AAAA)");
                    string fecha1 = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(fecha1) || !s.ValidarFormatoFecha(fecha1))
                    {
                        Console.WriteLine("Ingrese una fecha real (con formato DD/MM/AAAA) que no sea vacía");
                        fecha1 = Console.ReadLine();
                    }

                    Console.WriteLine("Ingrese la segunda fecha (formato: DD/MM/AAAA)");
                    string fecha2 = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(fecha2) || !s.ValidarFormatoFecha(fecha2))
                    {
                        Console.WriteLine("Ingrese una fecha real (con formato DD/MM/AAAA) que no sea vacía");
                        fecha2 = Console.ReadLine();
                    }

                    //Obtenemos una lista de post en el rango de fechas ingresadas a traves de un metodo en Sistema.
                    List<Post> ListaPostEntreFechas = s.ListarPostEntreFechas(fecha1, fecha2);
                    //Mostramos mensajes especificos de acuerdo al resultado:
                    if (ListaPostEntreFechas.Count == 0)
                    {
                        //Si no hay publicaciones entre las fechas mostramos lo siguiente.
                        Console.WriteLine("No hay publicaciones asociadas a ese rango de fechas");
                    }
                    else
                    {
                        //Si hay publicaciones entre las fechas mostramos lo siguiente.
                        Console.WriteLine($"Estas son las publicaciones asociadas al rango de fechas: ");
                        foreach (Post p in ListaPostEntreFechas)
                        {
                            Console.WriteLine(p.ToString());
                            Console.WriteLine("_______________");
                        }
                    }
                }
                catch (Exception e)
                {
                    //Mostramos un mensaje de error especifico al usuario en caso de que algun dato sea incorrecto.
                    Console.WriteLine(e.Message);
                }
            }


            //Metodo para case 5
            static void ListarMiembrosConMasPublicaciones(Sistema s)
            {
                //Obtenemos la lista de miembros con mas publicaciones mediante un metodo en sistema.
                List<Miembro> ListaMiembroConMasPublicaciones = s.MiembroConMasPublicaciones();
                //Mostramos mensajes especificos al usuario segun el resultado de la busqueda.
                if (ListaMiembroConMasPublicaciones.Count == 0)
                {
                    //Si no hay publicaciones asociadas a un miembro en el sistema.
                    Console.WriteLine("No hay publicaciones en el sistema");
                }
                else
                {
                    //Si hay uno o varios miembros que tengan la mayor cantidad de publicaciones.
                    Console.WriteLine($"El/los  miembro/s con mayor cantidad de publicaciones son : ");
                }
                Console.WriteLine("");
                foreach (Miembro m in ListaMiembroConMasPublicaciones)
                {
                    Console.WriteLine(m.ToString());
                    Console.WriteLine("_______________");
                }
            }

            #endregion
        }
    }
}
