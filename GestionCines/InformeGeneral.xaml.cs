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
    }
}
