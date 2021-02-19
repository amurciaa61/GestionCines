using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionCines
{
    /// <summary>
    /// Lógica de interacción para Utilidades.xaml
    /// </summary>
    public partial class Utilidades : Window
    {
        public string FormaDePago { get; set; }
        public Utilidades()
        {
            ServicioBaseDatos bbdd = new ServicioBaseDatos();
            InitializeComponent();
            DataContext = this;
            ObservableCollection<string> formasPago = bbdd.ObtenerFormaDePago();
            formaPagoComboBox.ItemsSource = formasPago;
        }
        private void CommandBinding_Executed_Aceptar(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
