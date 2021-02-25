using System.Windows;
using System.Windows.Input;

namespace GestionCines
{
    /// <summary>
    /// Lógica de interacción para InformeDetalle.xaml
    /// </summary>
    public partial class InformeDetalle : Window
    {
        private readonly InformeDetalleVM _vm;
        public InformeDetalle()
        {
            _vm = new InformeDetalleVM();
            InitializeComponent();
            DataContext = _vm;
        }
        private void CommandBinding_Executed_Filtrar(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.RefrescarFiltrado();
        }
        private void CommandBinding_Executed_Salir(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
