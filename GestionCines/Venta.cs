using System.ComponentModel;

namespace GestionCines
{
    class Venta : INotifyPropertyChanged
    {
        public int IDVENTA { get; set; }
        public Sesion SESION { get; set; }
        public int CANTIDAD { set; get; }
        public string PAGO { get; set; }

        public Venta()
        {
        }

        public Venta(int iDVenta, Sesion sesion, int cantidad, string pago)
        {
            IDVENTA = iDVenta;
            SESION = sesion;
            CANTIDAD = cantidad;
            PAGO = pago;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
