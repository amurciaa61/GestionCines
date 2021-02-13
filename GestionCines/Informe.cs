using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestionCines
{
    class Informe : INotifyPropertyChanged
    {
        public Pelicula PELICULA { get; set; }
        public string TITULO { get; set; }
        public Sesion SESION { get; set; }
        public Venta VENTA { get; set; }
        public Sala SALA { get; set; }
        public string NUMERO { get; set; }
        public int TICKET { get; set; }
        public int DISPONIBILIDAD { get; set; }
        public int CANTIDAD { get; set; }
        public int VENTAS { get; set; }
        public string HORA { get; set; }
        public string PAGO { get; set; }
        public string PORCENTAJEOCUPACION { get; set; }
        // Informe General
        public Informe(Pelicula pelicula,Sala sala,string hora,int ventas)
        {
            PELICULA = pelicula;
            SALA = sala;
            HORA = hora;
            VENTAS = ventas;
            DISPONIBILIDAD = sala.CAPACIDAD - ventas;
            double porcentaje = sala.CAPACIDAD != 0 ? (float)(ventas * 100) / sala.CAPACIDAD : 0;
            PORCENTAJEOCUPACION = FormatearValor(porcentaje,"D");
        }
        // Informe Detalle
        public Informe(string titulo,string hora,string numero,int ticket,int cantidad,string pago)
        {
            HORA = hora;
            TITULO = titulo;
            NUMERO = numero;
            TICKET = ticket;
            CANTIDAD = cantidad;
            PAGO = pago;
         }
        public static string FormatearValor(double valor,string tipo)
        {
            string salida="";
            switch (tipo)
            {
                case "D":
                    salida = $"{valor,7:F1}";
                    break;
                default:
                    break;
            }
            return salida;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
