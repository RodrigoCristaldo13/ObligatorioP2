# Soccer Social Network

Proyecto obligatorio para el curso de Programación II  
Universidad ORT Uruguay - Facultad de Ingeniería  
Analista en Tecnologías de la Información  
Grupo: N2F  
Autores: Rodrigo Cristaldo (310992), Angel Vaz (320162)  
Tutor: Andrés Ureta  
Fecha: Noviembre 2023

## Descripción

**Soccer Social Network** es una red social temática centrada en el fútbol. Permite a los usuarios registrarse como miembros, publicar contenido, interactuar con otros usuarios y, en el caso de los administradores, gestionar el comportamiento de los usuarios y moderar el contenido de la plataforma.

## Funcionalidades principales

### Usuario Anónimo
- Registro como miembro
- Inicio de sesión

### Usuario Miembro
- Crear publicaciones
- Comentar publicaciones
- Visualizar y editar su perfil
- Participar activamente en la comunidad

### Usuario Administrador
- Bloquear miembros (impide que publiquen o comenten)
- Banear publicaciones (eliminarlas del feed público)
- Acceso a vistas administrativas de miembros y publicaciones

## Diagrama de clases y casos de uso

El proyecto incluye un modelo robusto de dominio con relaciones claras entre entidades, así como un conjunto de casos de uso que representan las interacciones más relevantes dentro de la plataforma.

## Acceso a la aplicación

### Enlace (deploy en Somee)
[http://soccersocialnet.somee.com](http://soccersocialnet.somee.com)

### Credenciales

#### Miembros:
| Usuario | Email | Contraseña |
|--------|-------------------|----------|
| m1 - Rodrigo Cristaldo | rodrigo@gmail.com | rC1234 |
| m2 - Angel Vaz | angel@gmail.com | aV1234 |
| m3 - Edinson Cavani | edi@gmail.com | eC1234 |
| m4 - Diego Forlan | forlan@gmail.com | dF1234 |
| m5 - Sergio Ramos | sergio@gmail.com | sR1234 |
| m6 - Robert Lewandowski | robert@gmail.com | rL1234 |
| m7 - Cristiano Ronaldo | cr7@gmail.com | cR1234 |
| m8 - Neymar Santos | neymar@gmail.com | nS1234 |
| m9 - Luis Suarez | lucho@gmail.com | lS1234 |
| m10 - Lionel Messi | messi@gmail.com | lm1234 |
| m11 - Pato Sosa | patososa@gmail.com | pS1234 |

#### Administrador:
- Email: `admin1@gmail.com`
- Contraseña: `Admin4321`

## Testing

El sistema fue validado a través de una tabla de pruebas que cubre diferentes escenarios y usuarios (anónimo, miembro, administrador). Todas las pruebas pasaron correctamente, verificando la navegación, interacción y restricciones funcionales.

## Estructura del código

- **Dominio**: Clases de entidad, lógica de negocio y estructuras clave.
- **IU WebApp**: Interfaz de usuario con formularios, vistas y controladores.
- Proyecto desarrollado siguiendo el patrón MVC.

## Tecnologías utilizadas

- ASP.NET con Web Forms
- C#
- HTML, CSS
- Base de datos SQL Server
- Hosting gratuito en Somee.com

## Cómo ejecutar el proyecto localmente

1. Clonar el repositorio.
2. Abrir la solución en Visual Studio.
3. Restaurar los paquetes NuGet.
4. Asegurarse de tener SQL Server Express o LocalDB configurado.
5. Ejecutar migraciones o importar la base de datos si está incluida.
6. Iniciar el proyecto con IIS Express.

## Licencia

Proyecto educativo para uso académico. No está licenciado para uso comercial.
