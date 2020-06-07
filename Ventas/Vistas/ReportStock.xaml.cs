using System.Windows;
using System.Windows.Input;


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

        private void mover(object sender, MouseButtonEventArgs e) // permite el movimiento de la ventana dentro del sistema
        {
            this.DragMove();
        }

        private void btnCerrar(object sender, RoutedEventArgs e) //cierra la ventana
        {
            this.Close();
        }

        private void Ventana_reporte (object sender, RoutedEventArgs e) //crea conexion y muestra reporte realizado en el ide Crystal Report
        {
            ReporteStock abrir = new ReporteStock();
            abrir.Load(@"ReporteStock.rep");
            VerDatos.ViewerCore.ReportSource = abrir;

        }

    }
}
