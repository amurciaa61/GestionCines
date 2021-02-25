using System;
using System.Windows;
using System.Windows.Input;

namespace GestionCines
{
    /// <summary>
    /// Lógica de interacción para Sesiones.xaml
    /// </summary>
    public partial class Sesiones : Window
    {
        private readonly SesionesVM _vm;
        public Sesiones()
        {
            try
            {
                _vm = new SesionesVM();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + ". Pulse Aceptar para Salir.", "Errores", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
            }
            InitializeComponent();
            DataContext = _vm;
        }
        private void CommandBinding_Executed_Añadir(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.AñadirSesion();
        }
        private void CommandBinding_Executed_Editar(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.EditarSesion();
        }
        private void CommandBinding_Executed_Borrar(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Esta seguro que desea borrar la sesión?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    string borrado = _vm.BorrarSesion();
                    MessageBox.Show("Registro (" + borrado + ") borrado", "Baja", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        private void CommandBinding_CanExecute_Borrar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm.HaySesionSeleccionada() && !_vm.TieneVentas();
        }

        private void CommandBinding_CanExecute_Editar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm.HaySesionSeleccionada();
        }

        private void CommandBinding_Executed_GuardarCambios(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.GuardarCambios();
        }

        private void CommandBinding_CanExecute_GuardarCambios(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm.FormularioOk();
        }
        private void CommandBinding_Executed_Cancelar(object sender, ExecutedRoutedEventArgs e)
        {
            _vm.Cancelar();
        }

        private void CommandBinding_CanExecute_Cancelar(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _vm.HayDatos();
        }
        private void CommandBinding_Executed_Salir(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
