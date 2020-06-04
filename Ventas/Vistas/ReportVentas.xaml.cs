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

namespace ProyectoTienda
{
    /// <summary>
    /// Lógica de interacción para ReportVentas.xaml
    /// </summary>
    public partial class ReportVentas : Window
    {
        public ReportVentas()
        {
            InitializeComponent();
        }

        private void mover(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Vent_reporte(object sender, RoutedEventArgs e)
        {
            ReporteVentas abrir = new ReporteVentas();
            abrir.Load(@"ReporteVentas.rep");
            VerDatos.ViewerCore.ReportSource = abrir;

        }
    }
}
