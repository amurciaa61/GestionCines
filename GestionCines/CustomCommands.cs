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
        public static readonly RoutedUICommand Utilidades = new RoutedUICommand
            ("Utilidades",
            "Utilidades",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                        new KeyGesture(Key.U,ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand Editar = new RoutedUICommand
            ("Editar",
            "Editar",
            typeof(CustomCommands)
            );
        public static readonly RoutedUICommand Insertar = new RoutedUICommand
            ("Insertar",
             "Insertar",
             typeof(CustomCommands)
            );
        public static readonly RoutedUICommand Borrar = new RoutedUICommand
          ("Borrar",
           "Borrar",
           typeof(CustomCommands)
          );
        public static readonly RoutedUICommand GuardarCambios = new RoutedUICommand
            ("GuardarCambios",
            "GuardarCambios",
            typeof(CustomCommands)
            );
        public static readonly RoutedUICommand Cancelar = new RoutedUICommand
            ("Cancelar",
            "Cancelar",
            typeof(CustomCommands)
            );
        public static readonly RoutedUICommand Aceptar = new RoutedUICommand
            ("Aceptar",
            "Aceptar",
            typeof(CustomCommands)
            );
        public static readonly RoutedUICommand Sesiones = new RoutedUICommand
            ("Sesiones",
            "Sesiones",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                  new KeyGesture(Key.E, ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand InformeGeneral = new RoutedUICommand
            ("InformeGeneral",
            "InformeGeneral",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.G,ModifierKeys.Alt)
            }
            );
        public static readonly RoutedUICommand InformeDetalle = new RoutedUICommand
            ("InformeDetalle",
            "InformeDetalle",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D,ModifierKeys.Alt)
            }
            );
        public static readonly RoutedUICommand Ventas = new RoutedUICommand
            ("Ventas",
            "Ventas",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.V,ModifierKeys.Alt)
            }
            );
        public static readonly RoutedUICommand Filtrar = new RoutedUICommand
            ("Filtrar",
            "Filtrar",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S,ModifierKeys.Control)
            }
            );
    }
}
