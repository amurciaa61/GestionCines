using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bbdd = new ServicioBaseDatos();
            OFERTA = bbdd.ObtenerOfertaDisponible();
            PAGO = bbdd.ObtenerFormaDePago();
            VENTAFORMULARIO = new OfertaDisponible();
        }
        public void AñadirVenta()
        {
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
            VENTAFORMULARIO = new OfertaDisponible();
        }
        public bool HayDatos()
        {
            return VENTAFORMULARIO.DISPONIBILIDAD > 0;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
