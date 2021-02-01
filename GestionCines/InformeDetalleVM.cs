using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GestionCines
{
    class InformeDetalleVM : INotifyPropertyChanged
    {
        private readonly ServicioBaseDatos bbdd;
        public ObservableCollection<Informe> LISTA { get; set; }
        public InformeDetalleVM()
        {
            bbdd = new ServicioBaseDatos();
            LISTA = bbdd.ObtenerInformeDetalle();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
