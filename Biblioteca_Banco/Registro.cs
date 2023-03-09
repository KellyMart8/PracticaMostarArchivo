﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_Banco
{
    public class Registro
    {
        private int cuenta;
        private string primerNombre;
        private string apellidoPat;
        private decimal saldo;

        //El contructor sin parametros establece los mienbros a los valores predeterminados
        public Registro() : this(0, "", "", 0.0M)
        {

        }

        public Registro(int valorCuenta, string valorPrimerNombre, string valorApellido, decimal valorSaldo) 
        {
            Cuenta = valorCuenta;
            PrimerNombre = valorPrimerNombre;
            ApellidoPat = valorApellido;
            Saldo = valorSaldo;

        }

        public int Cuenta { get => cuenta; set => cuenta = value; }
        public string PrimerNombre { get => primerNombre; set => primerNombre = value; }
        public string ApellidoPat { get => apellidoPat; set => apellidoPat = value; }
        public decimal Saldo { get => saldo; set => saldo = value; }
    }
}
