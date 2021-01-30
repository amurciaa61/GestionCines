using System.ComponentModel;

namespace GestionCines
{
    class Sala : INotifyPropertyChanged
    {
        public int IDSALA { get; set; }
        public string NUMERO { get; set; }
        public int CAPACIDAD { get; set; }
        public bool DISPONIBLE { get; set; }

        public Sala()
        {
        }

        public Sala(int iDSala, string numero, int capacidad, bool disponible)
        {
            IDSALA = iDSala;
            NUMERO = numero;
            CAPACIDAD = capacidad;
            DISPONIBLE = disponible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
