using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GestionCines
{
    class InformeGeneralVM : INotifyPropertyChanged
    {
        public const string CONDICION_FIJA = " WHERE 1=1";
        
        private readonly ServicioBaseDatos bbdd;
        public ObservableCollection<Informe> LISTA { get; set; }
        public ObservableCollection<Pelicula> PELICULAS { get; set; }
        public ObservableCollection<Sala> SALAS { get; set; }
        public ObservableCollection<string> SESIONES { get; set; }
        public ObservableCollection<string> CALIFICACIONES { get; set; }
        public ObservableCollection<string> GENEROS { get; set; }
        public Pelicula PELICULASELECCIONADA { get; set; }
        public Sala SALASELECCIONADA { get; set; }
        public string SESIONSELECCIONADA { get; set; }
        public string CALIFICACIONSELECCIONADA { get; set; }
        public string GENEROSELECCIONADA { get; set; }
        public InformeGeneralVM()
        {
            PELICULASELECCIONADA = new Pelicula();
            SALASELECCIONADA = new Sala();
            SESIONSELECCIONADA = "";
            CALIFICACIONSELECCIONADA = "";
            GENEROSELECCIONADA = "";
            bbdd = new ServicioBaseDatos();
            LISTA = bbdd.ObtenerInformeGeneral(CONDICION_FIJA);
            PELICULAS = bbdd.ObtenerPeliculas(true);
            SALAS = bbdd.ObtenerSalas(false,true);
            SESIONES = bbdd.ObtenerSesionesFiltro();
            CALIFICACIONES = bbdd.ObtenerDatosFiltro("calificacion");
            GENEROS = bbdd.ObtenerDatosFiltro("genero");
        }
        public void RefrescarFiltrado()
        {
            string condicion_filtro = CONDICION_FIJA;
            if (PELICULASELECCIONADA.ID != 0)
                condicion_filtro += " AND p.idPelicula = '" + PELICULASELECCIONADA.ID + "'";
            if (SALASELECCIONADA.IDSALA != 0)
                condicion_filtro += " AND s.idSala = '" + SALASELECCIONADA.IDSALA + "'";
            if (SESIONSELECCIONADA.Length > 0)
                condicion_filtro += " AND se.hora = '" + SESIONSELECCIONADA + "'";
            if (CALIFICACIONSELECCIONADA.Length > 0)
                condicion_filtro += " AND p.calificacion = '" + CALIFICACIONSELECCIONADA + "'";
            if (GENEROSELECCIONADA.Length > 0)
                condicion_filtro += " AND p.genero = '" + GENEROSELECCIONADA + "'";
            LISTA = bbdd.ObtenerInformeGeneral(condicion_filtro);
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
