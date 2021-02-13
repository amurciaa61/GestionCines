using System.ComponentModel;

namespace GestionCines
{
    class Sesion : INotifyPropertyChanged
    {
        public int IDSESION { get; set; }
        public string NUMEROSALA { get; set; }
        public string TITULOPELICULA { get; set; }
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
            NUMEROSALA = sala.NUMERO;
            TITULOPELICULA = pelicula.TITULO;

        }
        public Sesion(Sesion sesion)
        {
            IDSESION = sesion.IDSESION;
            PELICULA = new Pelicula(sesion.PELICULA);
            SALA = new Sala(sesion.SALA);
            HORA = sesion.HORA;
            NUMEROSALA = SALA.NUMERO;
            TITULOPELICULA = PELICULA.TITULO;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
