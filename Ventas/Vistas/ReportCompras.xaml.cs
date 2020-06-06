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
    /// Lógica de interacción para ReportCompras.xaml
    /// </summary>
    public partial class ReportCompras: Window
    {
        public ReportCompras ()
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

        private void Ventana_report(object sender, RoutedEventArgs e)
        {
            ReporteCompras abrir = new ReporteCompras();
            abrir.Load(@"ReporteCompras.rep");
            VerDato.ViewerCore.ReportSource = abrir;

        }
    }
}
