using System.Windows.Input;

namespace GestionCines
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Ayuda = new RoutedUICommand
            ("Ayuda",
             "Ayuda",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F1,ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand Salir = new RoutedUICommand
            ("Salir",
            "Salir",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F4,ModifierKeys.Alt)
            }
            );
        public static readonly RoutedUICommand Salas = new RoutedUICommand
            ("Salas",
            "Salas",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S,ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand EditarSala = new RoutedUICommand
            ("EditarSala",
            "EditarSala",
            typeof(CustomCommands)
            );
        public static readonly RoutedUICommand InsertarSala = new RoutedUICommand
            ("InsertarSala",
             "InsertarSala",
             typeof(CustomCommands)
            );
        public static readonly RoutedUICommand GuardarCambiosSala = new RoutedUICommand
            ("GuardarCambiosSala",
            "GuardarCambiosSala",
            typeof(CustomCommands)
            );
        public static readonly RoutedUICommand Cancelar = new RoutedUICommand
            ("Cancelar",
            "Cancelar",
            typeof(CustomCommands)
            );
    }
}
