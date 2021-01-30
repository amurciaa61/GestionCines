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
                new KeyGesture(Key.S,ModifierKeys.Control)
            }
            );
    }
}
