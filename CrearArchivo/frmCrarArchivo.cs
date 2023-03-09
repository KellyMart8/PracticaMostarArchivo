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

namespace CrearArchivo
{
    public partial class frmCrarArchivo : frmBancoUI
    {
        private StreamWriter archivoWrite; //escribe datos en el archivo de texto
        private FileStream salida; //mantiene la conexion con el archivo

        public frmCrarArchivo()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //crar un cuadro de dialogo que permite al usuario guardar el archivop
            SaveFileDialog selectorAchivo = new SaveFileDialog();
            DialogResult resultado = selectorAchivo.ShowDialog();
            string nombreArchivo; //nombre del archivo en el que se va a guardar los datos

            selectorAchivo.CheckFileExists = false; //permite al usuario crear el archivo

            //sale el manejador de eventos
            if (resultado == DialogResult.Cancel)
                return;
            nombreArchivo = selectorAchivo.FileName; // obtiene el nombre del earchivo especificado

            //muestra el error si el usuario especifica un archivo invalido
            if (nombreArchivo == " " || nombreArchivo == null)
            {
                MessageBox.Show("Nombre de archivo invalido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Guarda el archivo mediante el  objerto FileStream, si el usuario
                //Especifico un archivo valido

                try
                {
                    //abre el archivo con acceso de escritura
                    salida = new FileStream(nombreArchivo, FileMode.OpenOrCreate, FileAccess.Write);

                    //Establece el archivo paraa escribir los datos
                    archivoWrite = new StreamWriter(salida);

                    //dessabilitar
                    btnGuardar.Enabled = false;
                    btnEntrar.Enabled = true;
                }
                //Maneja la excepcion si hay un problema al abrir un archivo
                catch (IOException)
                {
                    //notifica al usuario que el archivo noi exits
                    MessageBox.Show("Error al abrir el archivo", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) 
        {
           
        }

        //Manejador de eventos para entrar
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //Almacena el arreglo string de los controles textbox
            string[] valores = getValoresControles();

            //Regisdtro que contiene los valores delos controles Texxtbox
            Registro registro = new Registro();

            //determinar si el campo no esta vacia
            if (valores[(int)IndicesTextBox.CUENTA] != "")
            {
                try
                {
                    //Obtiene eel valor de numero cuenta
                    int numeroCuenta = int.Parse(valores[(int)IndicesTextBox.CUENTA]);

                    if (numeroCuenta > 0)
                    {
                        //Almaqcena loss campos texto en registro
                        registro.Cuenta = numeroCuenta;
                        registro.PrimerNombre = valores[(int)IndicesTextBox.NOMBRE];
                        registro.ApellidoPat = valores[(int)IndicesTextBox.APELLIDO];
                        registro.Saldo = decimal.Parse(valores[(int)IndicesTextBox.SALDO]);

                        //Escribe el registro, los campos por comas
                        archivoWrite.WriteLine(registro.Cuenta + "," +
                            registro.PrimerNombre + "," +
                            registro.ApellidoPat + "," +
                            registro.Saldo + ",");
                    }
                    else
                    {
                        //Notificar si el numero es invalido
                        MessageBox.Show("Numero de cuenta invalido", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Error al scribir n archivo", "Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FormatException) 
                {
                    MessageBox.Show("Formato invalido", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            LimpiarcontrolesTextBox();
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            //determinar si el archivop existe
            if (salida != null)
            {
                try
                {
                    archivoWrite.Close();
                    salida.Close();
                }
                //notifica al usuario del error al cerrar el archivo
                catch (IOException)
                {
                    MessageBox.Show("Error al cerrar el archivo", "Error",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Application.Exit();
        }
    }
}
