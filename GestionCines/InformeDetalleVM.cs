using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GestionCines
{
    class InformeDetalleVM : INotifyPropertyChanged
    {
        public ObservableCollection<Informe> LISTA { get; set; }
        ServicioBaseDatos bbdd;
        public InformeDetalleVM()
        {
            bbdd = new ServicioBaseDatos();
            LISTA = bbdd.ObtenerInformeDetalle();
        }
        public void RefrescarFiltrado()
        {
            LISTA = bbdd.ObtenerInformeDetalle();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
