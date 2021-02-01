using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestionCines
{
    class OfertaDisponible : INotifyPropertyChanged
    {
        public string PELICULA { get; set; }
        public string HORA { get; set; }
        public int NUMERO { get; set; }
        public int DISPONIBILIDAD { get; set; }
        public int IDSESION { get; set; }
        public int CANTIDAD { get; set; }
        public string PAGO { get; set; }
        public OfertaDisponible(string pelicula, string hora, int numero, int disponibilidad,int idSesion)
        {
            PELICULA = pelicula;
            HORA = hora;
            NUMERO = numero;
            DISPONIBILIDAD = disponibilidad;
            IDSESION = idSesion;
        }

        public OfertaDisponible(OfertaDisponible ofertadisponible)
        {
            PELICULA = ofertadisponible.PELICULA;
            HORA = ofertadisponible.HORA;
            NUMERO = ofertadisponible.NUMERO;
            DISPONIBILIDAD = ofertadisponible.DISPONIBILIDAD;
            IDSESION = ofertadisponible.IDSESION;
            PAGO = null;
            CANTIDAD = 0;

        }

        public OfertaDisponible()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
