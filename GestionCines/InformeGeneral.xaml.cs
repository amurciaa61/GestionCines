using System.Windows;
using System.Windows.Input;

namespace GestionCines
{
    /// <summary>
    /// Lógica de interacción para InformeGeneral.xaml
    /// </summary>
    public partial class InformeGeneral : Window
    {
        private readonly InformeGeneralVM _vm;
        public InformeGeneral()
        {
            _vm = new InformeGeneralVM();
            InitializeComponent();
            DataContext = _vm;
        }

        private void CommandBinding_Executed_Filtrar(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.RefrescarFiltrado();
        }
    }
}
