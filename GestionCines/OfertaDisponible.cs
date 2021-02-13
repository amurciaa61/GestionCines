using System.ComponentModel;

namespace GestionCines
{
    class OfertaDisponible : INotifyPropertyChanged
    {
        public string IMAGEN { get; set; }
        public string PELICULA { get; set; }
        public string HORA { get; set; }
        public string NUMERO { get; set; }
        public int DISPONIBILIDAD { get; set; }
        public int IDSESION { get; set; }
        public int CANTIDAD { get; set; }
        public string PAGO { get; set; }
        public OfertaDisponible(string pelicula, string hora, string numero, int disponibilidad,int idSesion,string imagen)
        {
            PELICULA = pelicula;
            HORA = hora;
            NUMERO = numero;
            DISPONIBILIDAD = disponibilidad;
            IDSESION = idSesion;
            IMAGEN = imagen;
        }

        public OfertaDisponible(OfertaDisponible ofertadisponible)
        {
            PELICULA = ofertadisponible.PELICULA;
            HORA = ofertadisponible.HORA;
            NUMERO = ofertadisponible.NUMERO;
            DISPONIBILIDAD = ofertadisponible.DISPONIBILIDAD;
            IDSESION = ofertadisponible.IDSESION;
            PAGO = "Efectivo";
            CANTIDAD = 0;
            IMAGEN = ofertadisponible.IMAGEN;

        }

        public OfertaDisponible()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
