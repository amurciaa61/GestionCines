using System.ComponentModel;

namespace GestionCines
{
    class Sesion : INotifyPropertyChanged
    {
        public int IDSESION { get; set; }
        public Pelicula PELICULA { get; set; }
        public Sala SALA { get; set; }
        public string HORA { get; set; }

        public Sesion()
        {
        }

        public Sesion(int iDSesion, Pelicula pelicula, Sala sala, string hora)
        {
            IDSESION = iDSesion;
            PELICULA = pelicula;
            SALA = sala;
            HORA = hora;
        }
        public Sesion(Sesion sesion)
        {
            IDSESION = sesion.IDSESION;
            PELICULA = sesion.PELICULA;
            SALA = sesion.SALA;
            HORA = sesion.HORA;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
