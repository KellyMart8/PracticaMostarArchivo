using Biblioteca_Banco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmLeerArchivo
{
    public partial class frmLeerArchivo : frmBancoUI
    {
        private FileStream entrada; //Mantiene la conexion a un archivo
        private StreamReader archivoReader; //lee datos de un achivo de texto
        public frmLeerArchivo()
        {
            InitializeComponent();
        }

        private void btnAbrirArchivo_Click(object sender, EventArgs e)
        {
            //Creea un cuadro de  dialgo que permite al usuario abrir el archivo
            OpenFileDialog selectorArchivo = new OpenFileDialog();
            DialogResult resultado = selectorArchivo.ShowDialog();
            string nombreArchivo; //nombre del archivo que contiene los datos

            //Sale el manejadpor de eventosa si el usuario hace clic en cancelar
            if (resultado == DialogResult.Cancel)
                return;
            nombreArchivo = selectorArchivo.FileName; //Obtiene al nombre de archivo especificado
            LimpiarcontrolesTextBox();

            //Muestra merror dsi el usuario especifica un archivo invalido
            if (nombreArchivo == "" || nombreArchivo == null)
                MessageBox.Show("NOMBRE DE ARCHIVO INVALDO", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {

                    //Crea objeto Files Stream para obtenmer acceso de lectura del rchivo
                    entrada = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read);

                    //establece el archivo del que se van a leer los datos
                    archivoReader = new StreamReader(entrada);

                    btnAbrirArchivo.Enabled = false; //deshabilita
                    btnSiguiente.Enabled = true; //Habilita
                }
                catch (IOException)
                {
                    MessageBox.Show("Error al abrir el archivo", "ERROR",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtiene el siguiente registrodisponible en el archivop
                string registroEntrada = archivoReader.ReadLine();
                string[] camposEntrada; //almacena partes individuales de datos

                if (registroEntrada != null)
                {
                    camposEntrada = registroEntrada.Split(',');
                    Registro registro = new Registro(Convert.ToInt32(camposEntrada[0]), camposEntrada[1], camposEntrada[2], Convert.ToDecimal(camposEntrada[3]));

                    //Copia los vaslores dela rreglo string a los vsalores de los controles textbox
                    EstablecerValores(camposEntrada);
                }
                else
                {
                    archivoReader.Close();  // cierra StreamReader
                    entrada.Close();        //cierra FileStrea si no hay registro en el marchivo
                    btnAbrirArchivo.Enabled = true; //habilita el boton
                    btnSiguiente.Enabled = false;

                    LimpiarcontrolesTextBox();

                    MessageBox.Show("No hay mas registros", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Error al leer el archivo", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
