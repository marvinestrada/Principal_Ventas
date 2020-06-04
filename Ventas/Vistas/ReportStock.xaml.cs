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
    /// Lógica de interacción para Pagos.xaml
    /// </summary>
    public partial class ReportStock : Window
    {
        public ReportStock()
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

        private void Ventana_reporte (object sender, RoutedEventArgs e)
        {
            ReporteStock abrir = new ReporteStock();
            abrir.Load(@"ReporteStock.rep");
            VerDatos.ViewerCore.ReportSource = abrir;

        }

    }
}
