using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCines
{
    class SesionesVM : INotifyPropertyChanged
    {
        public Sesion SESIONSELECCIONADA { get; set; }
        public Sesion SESIONFORMULARIO { get; set; }
        public ObservableCollection<Sesion> SESIONES { get; set; }
        public ObservableCollection<Pelicula> PELICULAS { get; set; }
        public ObservableCollection<Sala> SALAS { get; set; }
        public ObservableCollection<string> TITULOS { get; set; }
        public ObservableCollection<string> SALANUMERO { get; set; }
        public ObservableCollection<string> HORAS { get; set; }

        public Modo ACCION { get; set; }

        private readonly ServicioBaseDatos bbdd;

        public SesionesVM()
        {
            try
            {
                bbdd = new ServicioBaseDatos();
                PELICULAS = bbdd.ObtenerPeliculas(false);
                InicializarVariables();
                ACCION = Modo.Insertar;
            }
            catch (Exception ex)
            {
                throw new MisExcepciones(ex.Message);
            }

        }
        public void AñadirSesion()
        {
            SESIONFORMULARIO = new Sesion();
            ACCION = Modo.Insertar;
        }
        public void EditarSesion()
        {
            SESIONFORMULARIO = new Sesion(SESIONSELECCIONADA);
            ACCION = Modo.Actualizar;
        }
        public string BorrarSesion()
        {
            string mensajeBorre = SESIONSELECCIONADA.IDSESION +" "+SESIONSELECCIONADA.TITULOPELICULA + 
                                   " " + SESIONSELECCIONADA.NUMEROSALA +" "+SESIONSELECCIONADA.HORA;
            SESIONFORMULARIO = new Sesion(SESIONSELECCIONADA);
            bbdd.BorrarSesion(SESIONFORMULARIO);
            InicializarVariables();
            ACCION = Modo.Borrar;
            return mensajeBorre;
        }
        public bool HaySesionSeleccionada()
        {
            return SESIONSELECCIONADA != null;
        }
        public bool TieneVentas()
        {
            return (HaySesionSeleccionada() && bbdd.ObtenerVentasPorSesion(SESIONSELECCIONADA.IDSESION) > 0);
        }
        public bool FormularioOk()
        {
            return SESIONFORMULARIO.HORA != "" &&
                   SESIONFORMULARIO.HORA != null &&
                   SESIONFORMULARIO.TITULOPELICULA != null &&
                   SESIONFORMULARIO.NUMEROSALA != null &&
                   ACCION != Modo.Borrar;
        }
        public void GuardarCambios()
        {
            SESIONFORMULARIO.SALA = bbdd.ObtenerSalaPorNumero(SESIONFORMULARIO.NUMEROSALA);
            SESIONFORMULARIO.PELICULA = bbdd.ObtenerPeliculaPorTitulo(SESIONFORMULARIO.TITULOPELICULA);
            if (ACCION == Modo.Insertar)
                bbdd.InsertarSesion(SESIONFORMULARIO);
            else
                bbdd.ActualizarSesion(SESIONFORMULARIO);
            InicializarVariables();
        }
        public void Cancelar()
        {
            SESIONFORMULARIO = new Sesion();
        }
        public bool HayDatos()
        {
            return SESIONFORMULARIO.HORA != null;
        }
        public void InicializarVariables()
        {
            SESIONFORMULARIO = new Sesion();
            SESIONES = bbdd.ObtenerSesiones();

            SALAS = bbdd.ObtenerSalas(true, false);
            HORAS = bbdd.ObtenerHoras(false);
            SALANUMERO = new ObservableCollection<string>();
            foreach (Sala salas in SALAS)
            {
                SALANUMERO.Add(salas.NUMERO);
            }
            TITULOS = new ObservableCollection<string>();
            foreach (Pelicula peliculas in PELICULAS)
            {
                TITULOS.Add(peliculas.TITULO);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
