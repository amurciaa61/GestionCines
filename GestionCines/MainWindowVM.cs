using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                bbdd.InsertarControlCargaPeliculas();
                bbdd.EliminarControlesCargaPeliculas();
                bbdd.EliminarCartelera();
                bbdd.CargarPeliculas(PELICULAS);
            }
            else
            {
                PELICULAS = bbdd.ObtenerPeliculas();
            }
        }

        public void Ayuda()
        {
            
        }
        public bool PuedoMostrarAyuda()
        {
            return true;
        }
        public void Salas(MainWindow mainWindow)
        {
            Salas salas = new Salas();
            salas.Owner = mainWindow;
            salas.Show();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
