using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GestionCines
{
    class VentasVM : INotifyPropertyChanged
    {
        public OfertaDisponible VENTASELECCIONADA { get; set; }
        public OfertaDisponible VENTAFORMULARIO { get; set; }
        public ObservableCollection<OfertaDisponible> OFERTA { get; set; }
        public ObservableCollection<string> PAGO { get; set; }
        private readonly ServicioBaseDatos bbdd;

        public VentasVM()
        {
            try
            {
                bbdd = new ServicioBaseDatos();
                OFERTA = bbdd.ObtenerOfertaDisponible();
                PAGO = bbdd.ObtenerFormaDePago();
                VENTAFORMULARIO = new OfertaDisponible();
            }catch(Exception ex)
            {
                throw new MisExcepciones(ex.Message);
            }
            
        }
        public void AñadirVenta()
        {
            // Refrescar dato de disponibilidad de la sala
            VENTASELECCIONADA.DISPONIBILIDAD = bbdd.DisponibilidadSalaSesion(VENTASELECCIONADA.NUMERO, VENTASELECCIONADA.IDSESION);
            VENTAFORMULARIO = new OfertaDisponible(VENTASELECCIONADA);
        }
        public bool HayOfertaDisponibleSeleccionada()
        {
            return VENTASELECCIONADA != null;
        }
        public bool FormularioOk()
        {
            return VENTAFORMULARIO.CANTIDAD <= VENTAFORMULARIO.DISPONIBILIDAD &&
                   VENTAFORMULARIO.CANTIDAD > 0 && VENTAFORMULARIO.PAGO != null;
        }
        public void GuardarCambios()
        {
            bbdd.InsertarVenta(VENTAFORMULARIO);
            VENTAFORMULARIO = new OfertaDisponible();
            OFERTA = bbdd.ObtenerOfertaDisponible();
        }
        public void Cancelar()
        {
            OFERTA = bbdd.ObtenerOfertaDisponible();
            VENTAFORMULARIO = new OfertaDisponible();
        }
        public bool HayDatos()
        {
            return VENTAFORMULARIO.PELICULA != null;
        }
        public void InformeDetalle(Ventas ventasWindow)
        {
            InformeDetalle informe = new InformeDetalle();
            informe.Owner = ventasWindow;
            informe.Show();
        }
        public void InformeGeneral(Ventas mainWindow)
        {
            InformeGeneral informe = new InformeGeneral();
            informe.Owner = mainWindow;
            informe.Show();
        }
        public void RefrescarFiltrado()
        {
            OFERTA = bbdd.ObtenerOfertaDisponible();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
