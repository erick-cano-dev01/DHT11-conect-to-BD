using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCapa
{
    public class Conexion
    {
        /*
         * La conexion nos permite conectarnos a la base de datos
         * 
         * @Data Source para el nombre del servidor
         * @Initial Catalog para lo que es el nombre de la base de datos
         * @Integrated Security para la integridad de seguridad
         */
        public static string Cn = "Data Source = LAP-ERICK20; " +
            "Initial Catalog = DHT11;" +
            "Integrated Security = true";
    }
}
