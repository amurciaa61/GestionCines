using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GestionCines
{
    class InformeGeneralVM : INotifyPropertyChanged
    {
        public ObservableCollection<Informe> LISTA { get; set; }
        private readonly ServicioBaseDatos bbdd;
        public InformeGeneralVM()
        {
            bbdd = new ServicioBaseDatos();
            LISTA = bbdd.ObtenerInformeGeneral();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
