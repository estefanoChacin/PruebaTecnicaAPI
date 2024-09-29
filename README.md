# PruebaTecnicaAPI

## Descripción de la API
Esta API fue desarrollada en .NET Core 8 siguiendo una arquitectura en N Capas. Está diseñada para gestionar usuarios, roles, categorías y productos, implementando autenticación basada en JWT (JSON Web Tokens). La API asigna diferentes niveles de acceso según los roles de los usuarios.

### Entidades principales
- **Usuarios**: Los usuarios están asociados a un rol y son quienes interactúan con la API.
- **Roles**: Un rol determina los permisos de acceso de los usuarios. Los roles disponibles son:
  - **ADMIN**: Tiene acceso total a todas las funcionalidades de la API.
  - **EMPLEADO**: Tiene acceso restringido, pudiendo crear, leer, actualizar y eliminar categorías y productos.
- **Categorías**: Agrupan los productos bajo diferentes categorías (por ejemplo, Electrónica, Hogar, etc.).
- **Productos**: Representan los artículos asociados a las categorías.

## Cómo ejecutar la aplicación
Para ejecutar la API, sigue los siguientes pasos:

1. Clona el proyecto en tu máquina local.
2. Asegúrate de tener instalado **Visual Studio 2022** y **MySQL 8.0**.
3. Una vez clonado el proyecto, dirígete al archivo `appsettings.json` de la capa **PruebaAPI** y busca la sección **ConnectionStrings**. Allí encontrarás la cadena de configuración para conectarte a tu motor de MySQL.
4. Modifica el nombre de usuario y la contraseña con los valores de tu servidor de base de datos.
5. Ejecuta el proyecto desde Visual Studio.

## Configuración de la base de datos
El proyecto contiene una migración pendiente. Al ejecutar la API, si la conexión a la base de datos es exitosa, se creará automáticamente la base de datos con el nombre **dbpruebatecnica**. Además, se insertarán datos iniciales, como roles, usuarios, categorías y algunos productos. Esto te permitirá iniciar sesión en la API y realizar pruebas.

**Credenciales de los usuarios registrados:**
1. **Email**: miguel@gmail.com, **Contraseña**: 12345, **Rol**: EMPLEADO.
2. **Email**: alejandro@gmail.com, **Contraseña**: 12345, **Rol**: ADMIN.

Una vez realizada esta configuración, no es necesario realizar ajustes adicionales en la base de datos.

## Ejemplo de solicitudes
El proyecto utiliza **Swagger** para documentar la API. Allí podrás encontrar toda la información sobre los endpoints y los JSON de entrada esperados.

Si prefieres no utilizar Swagger, en el proyecto, dentro de la capa de utilidades **PruebaAPI.UTILITY**, encontrarás una carpeta llamada **TestAPI**. Dentro de esta, se encuentra un archivo `.json` llamado **EndPoints PruebaTecnicaAPI.postman_collection.json**. Este archivo contiene una colección de **POSTMAN** que puedes importar en tu instancia de Postman para probar la API. Cada solicitud de la colección incluye ejemplos de los JSON que debes enviar en las solicitudes.
