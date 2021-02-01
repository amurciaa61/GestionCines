using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GestionCines
{
    class InformeGeneralVM : INotifyPropertyChanged
    {
        private readonly ServicioBaseDatos bbdd;
        public ObservableCollection<Informe> LISTA { get; set; }
        public InformeGeneralVM()
        {
            bbdd = new ServicioBaseDatos();
            LISTA = bbdd.ObtenerInformeGeneral();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
