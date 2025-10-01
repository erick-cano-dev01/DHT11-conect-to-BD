Proyecto Sensor DHT11 con C# y Windows Forms
Este proyecto permite la lectura de datos de temperatura y humedad mediante el sensor DHT11, registrando la información en una base de datos. Está desarrollado en C# utilizando Windows Forms y una arquitectura en tres capas (Presentación, Lógica de Negocio y Acceso a Datos).

Arquitectura
Capa de Presentación: Interfaz gráfica en Windows Forms.

Capa de Lógica de Negocio: Procesa y valida los datos del sensor.

Capa de Acceso a Datos: Conecta y guarda los datos en la base de datos.

Requisitos
Sensor DHT11 conectado a un microcontrolador (por ejemplo, Arduino).

Comunicación serial con la PC.

Visual Studio con .NET Framework.

SQL Server o base de datos compatible.
Clona el repositorio:

bash
1. git clone https://github.com/tuusuario/proyecto-dht11.git
Abre el proyecto en Visual Studio.

Importante: Antes de ejecutar, debes configurar la cadena de conexión en la capa de datos:

2. Abre el archivo Conexion.cs o donde esté definida la cadena de conexión.

Cambia el nombre de la base de datos (Initial Catalog) y el origen de datos (Data Source) según tu entorno local.

csharp
string cadenaConexion = "Data Source=TU_SERVIDOR;Initial Catalog=TU_BASE_DE_DATOS;Integrated Security=True;";

Ejecución

3. Ejecuta el proyecto desde Visual Studio.

Asegúrate de que el microcontrolador esté enviando datos por el puerto serial.

Los datos se mostrarán en la interfaz y se guardarán en la base de datos.

Pruebas

4. Verifica que los datos se reciban correctamente desde el puerto serial.

Confirma que se registren en la base de datos.

Prueba la interfaz con distintos valores simulados.
