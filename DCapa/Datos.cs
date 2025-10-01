using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCapa
{
    public class Datos
    {
        //Atributos que usaremos en los datos
        private string _temperatura;
        private string _humedad;
        private string _fecha;
        private string _hora;

        //Encapsulamiento de datos
        public string Temperatura { get => _temperatura; set => _temperatura = value; }
        public string Humedad { get => _humedad; set => _humedad = value; }
        public string Fecha { get => _fecha; set => _fecha = value; }
        public string Hora { get => _hora; set => _hora = value; }

        //Constructor vacio
        public Datos() { }

        //Utilizamos constructor con parametros
        public Datos(string temperatura, string humedad, string fecha, string hora)
        {
            Temperatura = temperatura;
            Humedad = humedad;
            Fecha = fecha;
            Hora = hora;
        }

        //Insercion de datos en la BD
        public string insertar(Datos sensor)
        {
            string rpta = "";
            SqlConnection con = new SqlConnection();
            try
            {
                //Abrimos conexion con SQL
                con.ConnectionString = Conexion.Cn;
                con.Open();

                //Establecer el comando
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = con;
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.CommandText = "seingresar_historial";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                //Parametros para el procedimiento almacenado
                SqlParameter ParTemperatura = new SqlParameter();
                ParTemperatura.ParameterName = "@temperatura";
                ParTemperatura.SqlDbType = SqlDbType.VarChar;
                ParTemperatura.Size = 50;
                ParTemperatura.Value = sensor.Temperatura;
                sqlcmd.Parameters.Add(ParTemperatura);

                SqlParameter ParHumedad = new SqlParameter();
                ParHumedad.ParameterName = "@humedad";
                ParHumedad.SqlDbType = SqlDbType.VarChar;
                ParHumedad.Size = 50;
                ParHumedad.Value = sensor.Humedad;
                sqlcmd.Parameters.Add(ParHumedad);

                /*
                 * Se realizan estas conversiones para que la BD acepte los datos
                 */
                // Fecha: convertir string "yyyy-MM-dd" a DateTime
                SqlParameter ParFecha = new SqlParameter();
                ParFecha.ParameterName = "@fecha";
                ParFecha.SqlDbType = SqlDbType.Date;
                ParFecha.Value = DateTime.ParseExact(sensor.Fecha, "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture);
                sqlcmd.Parameters.Add(ParFecha);

                // Hora: convertir string "HH:mm:ss" a TimeSpan
                SqlParameter ParHora = new SqlParameter();
                ParHora.ParameterName = "@hora";
                ParHora.SqlDbType = SqlDbType.Time;
                ParHora.Value = TimeSpan.ParseExact(sensor.Hora, "hh\\:mm\\:ss", 
                    CultureInfo.InvariantCulture);
                sqlcmd.Parameters.Add(ParHora);


                /*
                 * Se espera respuesta del comando
                 * @return OK si es correcto
                 * @return No si no se ingreso el registro
                 */
                rpta = sqlcmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingreso el registro";
            }
            catch (Exception ex) 
            {
                rpta = ex.Message;
            }
            //Cerramos la conexion
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return rpta; //@rpta
        }

        //Mostrar los primeros 100 registros
        public DataTable mostrarTop()
        {
            DataTable DtResultado = new DataTable("[Historial Sensor]");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexion.Cn;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlCon;
                sqlcmd.CommandText = "seseleccionar100_historial";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlcmd);
                sqlDat.Fill(DtResultado); //Llena el DataTable con los datos
            }
            catch (Exception ex)
            {
                DtResultado = null; //@return null como excepcion
            }
            return DtResultado; //@return la tabla
        }

        //Mostrar todos los registros
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("[Historial Sensor]");
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = Conexion.Cn;
                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlCon;
                sqlcmd.CommandText = "seseleccionartodos_historial";
                sqlcmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlDat = new SqlDataAdapter(sqlcmd);
                sqlDat.Fill(DtResultado); //Llena el DataTable con los datos
            }
            catch (Exception ex)
            {
                DtResultado = null; //@return null como excepcion
            }
            return DtResultado; //@return la tabla
        }
    }
}
