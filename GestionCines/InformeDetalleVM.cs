using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GestionCines
{
    class InformeDetalleVM : INotifyPropertyChanged
    {
        public ObservableCollection<Informe> LISTA { get; set; }
        public InformeDetalleVM()
        {
            ServicioBaseDatos bbdd = new ServicioBaseDatos();
            LISTA = bbdd.ObtenerInformeDetalle();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
