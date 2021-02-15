using System.Windows;
using System.Windows.Input;

namespace GestionCines
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        private readonly VentasVM _vm;
        public Ventas()
        {
            _vm = new VentasVM();
            InitializeComponent();
            DataContext = _vm;
        }

        private void CommandBinding_Executed_GuardarCambios(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.GuardarCambios();
        }

        private void CommandBinding_CanExecute_GuardarCambios(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm.FormularioOk();
        }

        private void CommandBinding_Executed_Insertar(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.AñadirVenta();
        }
        private void CommandBinding_CanExecute_Insertar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm.HayOfertaDisponibleSeleccionada();
        }
        private void CommandBinding_Executed_Cancelar(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.Cancelar();
        }

        private void CommandBinding_CanExecute_Cancelar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm.HayDatos();
        }
        private void CommandBinding_Executed_InformeDetalle(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.InformeDetalle(this);
        }
        private void CommandBinding_Executed_InformeGeneral(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.InformeGeneral(this);
        }
        private void CommandBinding_Executed_Filtrar(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.RefrescarFiltrado();
        }
    }
}
