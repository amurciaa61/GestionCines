using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;

namespace GestionCines
{
    class MainWindowVM : INotifyPropertyChanged
    {
        public bool HAYPELICULASCARGADAS { get; set; }
        public ObservableCollection<Pelicula> PELICULAS { get; set; }
        private readonly ServicioBaseDatos bbdd;
        public MainWindowVM()
        {
            bbdd = new ServicioBaseDatos();
            ServicioPeliculaGet servicioPeliculaAPI = new ServicioPeliculaGet();

            HAYPELICULASCARGADAS = bbdd.ComprobarCargaPeliculas();

            if (!HAYPELICULASCARGADAS)
            {
                HAYPELICULASCARGADAS = true;
                PELICULAS = servicioPeliculaAPI.ObtenerCartelera();
                if (PELICULAS == null)
                {
                    throw new MisExcepciones("No hay peliculas cargadas. Contactar con departamento técnico para que revise la conexión a Internet");
                }
                bbdd.InsertarControlCargaPeliculas();
                bbdd.EliminarControlesCargaPeliculas();
                bbdd.EliminarCartelera();
                bbdd.CargarPeliculas(PELICULAS);
                bbdd.RestaurarSesiones();
            }
            else
            {
                PELICULAS = bbdd.ObtenerPeliculas(false);
            }
        }

        public void Ayuda()
        {
            Help.ShowHelp(null, "GestionCines.chm");
        }

        public void Salas(MainWindow mainWindow)
        {
            Salas salas = new Salas();
            salas.Owner = mainWindow;
            salas.Show();
        }
        public void Sesiones(MainWindow mainWindow)
        {
            Sesiones sesiones = new Sesiones();
            sesiones.Owner = mainWindow;
            sesiones.Show();
        }
        public void InformeGeneral(MainWindow mainWindow)
        {
            InformeGeneral informe = new InformeGeneral();
            informe.Owner = mainWindow;
            informe.Show();
        }
        public void InformeDetalle(MainWindow mainWindow)
        {
            InformeDetalle informe = new InformeDetalle();
            informe.Owner = mainWindow;
            informe.Show();
        }
        public void Ventas(MainWindow mainWindow)
        {
            Ventas ventas = new Ventas();
            ventas.Owner = mainWindow;
            ventas.Show();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
