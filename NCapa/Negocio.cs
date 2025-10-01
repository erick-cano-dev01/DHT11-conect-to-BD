using DCapa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCapa
{
    public class Negocio
    {
        //Método insertar que llama al método insertar de la clase
        public string insertar(string temperatura, string humedad,
            string fecha, string hora)
        {
            Datos Obj = new DCapa.Datos();
            Obj.Temperatura = temperatura;
            Obj.Humedad = humedad;
            Obj.Fecha = fecha;
            Obj.Hora = hora;
            return Obj.insertar(Obj);
        }

        //LLamada a los metodos de mostrar
        public static DataTable MostrarTop()
        {
            return new Datos().mostrarTop();
        }
        public static DataTable MostrarTodos()
        {
            return new Datos().Mostrar();
        }
    }
}
