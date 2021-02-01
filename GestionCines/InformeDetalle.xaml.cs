using System;
using System.Collections.Generic;
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
    }
}
