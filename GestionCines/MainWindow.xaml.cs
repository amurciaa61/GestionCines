using System;
using System.Windows;
using System.Windows.Input;


namespace GestionCines
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowVM _vm; 
        public MainWindow()
        {
            // Instanciamos la clase VistaModelo
            try
            {
                _vm = new MainWindowVM();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message+ ". Pulse Aceptar para Salir.", "Errores", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
            InitializeComponent();
            DataContext = _vm;
        }

        private void CommandBinding_Executed_Ayuda(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.Ayuda();
        }
        private void CommandBinding_Executed_Utilidades(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.Utilidades(this);
        }

        private void CommandBinding_Executed_Salir(object sender, ExecutedRoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void CommandBinding_CanExecute_Salir(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Salas(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.Salas(this);
        }

        private void CommandBinding_Executed_Sesiones(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.Sesiones(this);
        }

        private void CommandBinding_Executed_InformeGeneral(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.InformeGeneral(this);
        }
        private void CommandBinding_Executed_InformeDetalle(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.InformeDetalle(this);
        }
        private void CommandBinding_Executed_Ventas(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.Ventas(this);
        }


    }
}
