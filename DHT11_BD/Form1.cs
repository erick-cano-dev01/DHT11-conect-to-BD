using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NCapa;

namespace DHT11_BD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Actualizar(); //Actualizamos cada que iniciamos el programa
        }

        /*
         * Actualiza el combo box
         * Busca los puertos disponibles
         * 
         * Se agregan con un foreach al combobox
         */
        private void Actualizar()
        {
            txtRpta.AppendText("Buscando puertos disponibles." + Environment.NewLine);
            cmbPuertos.Items.Clear();
            foreach (var item in System.IO.Ports.SerialPort.GetPortNames())
            {
                cmbPuertos.Items.Add(item);
            }
            //Si no hay puertos...
            if(cmbPuertos.Items.Count == 0)
            {
                txtRpta.AppendText("No se detectaron puertos." + Environment.NewLine);
            } else if(cmbPuertos.Items.Count != 0)
            {
                txtRpta.AppendText("Puertos agregados." + Environment.NewLine);
            }
        }

        /*
         * Se recibien los datos del serial aqui
         * 
         * Creamos variables para almacenar nuestros datos y mandarlos a la BD
         * -Almacenamos los datos leidos del serial en variables
         * -@Obtenemos fechas actuales y les damos formato
         * -Insertamos los datos cada minuto
         */
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                Negocio negocio = new Negocio();
                string rpta = "";
                string humedad = "";
                string temperatura = "";

                if (serialPort1.BytesToRead > 0)
                {
                    //Almacenamos los datos y les damos un formato
                    /*
                     * Para la lectura de cada dato
                     * 
                     * Leemos la primer linea donde contiene la humedad
                     * La segunda linea lee la temperatura
                     * Almacenamos esos datos en variables separadas
                     */
                    String datos = serialPort1.ReadLine();
                    String datos2 = serialPort1.ReadLine();
                    if (datos.Contains("H:") && datos2.Contains("T:"))
                    {
                        humedad = datos.Replace("H: ", "RH: ");
                        temperatura = datos2.Replace("T: ", "C°: ");
                    }

                    //Formato de fecha para introducir en la BD
                    DateTime fechaAhora = DateTime.Now;
                    string fecha = fechaAhora.ToString("yyyy-MM-dd");
                    string hora = fechaAhora.ToString("hh:mm:ss");

                    //Envio de datos a la capa de Negocio
                    if (!string.IsNullOrEmpty(humedad) && !string.IsNullOrEmpty(temperatura))
                    {
                        rpta = negocio.insertar(temperatura, humedad,
                        fecha, hora);
                        txtRpta.AppendText(rpta + Environment.NewLine);
                    }
                    else
                    {
                        txtRpta.AppendText("No se inserto, falto leer un valor." + Environment.NewLine);
                    }
                }
            }
            catch(Exception ex)
            {
                txtRpta.AppendText(ex.Message + ex.StackTrace + Environment.NewLine);
            }
        }

        /*
         * Boton que inicia la comunicacion con el puerto serial
         * @return fuera si no hay puertos seleccionados
         * 
         * Si esta abierto se manda un mensaje en el rpta para notificar
         * que el puerto esta abierto
         */
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cmbPuertos.Text.Equals(""))
            {
                txtRpta.AppendText("No hay un puerto seleccionado." + Environment.NewLine);
                return;
            }

            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = cmbPuertos.Text;
                serialPort1.Open();
                txtRpta.AppendText("Abriendo puerto..." + Environment.NewLine +
                    "Puerto abierto." + Environment.NewLine);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }
        //Metodo de mostrar los top 100 registros
        private void MostrarTop()
        {
            this.dataListado.DataSource = Negocio.MostrarTop();
        }
        //Metodo de mostrar todos los registros
        private void Mostrar()
        {
            this.dataListado.DataSource = Negocio.MostrarTodos();
        }
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            this.MostrarTop(); //Llamada al metodo MostrarTop
        }

        private void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            this.Mostrar(); //Llamada al metodo Mostrar
        }
    }
}
