using System.Windows;
using System.Windows.Input;


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

        private void mover(object sender, MouseButtonEventArgs e) // permite el movimiento de la ventana dentro del sistema 
        {
            this.DragMove();
        }

        private void btnCerrar(object sender, RoutedEventArgs e) //cierra la ventana 
        {
            this.Close();
        }

        private void Ventana_report(object sender, RoutedEventArgs e) //crea conexion y muestra reporte realizado en el ide Crystal Report
        {
            ReporteCompras abrir = new ReporteCompras();
            abrir.Load(@"ReporteCompras.rep");
            VerDato.ViewerCore.ReportSource = abrir;

        }
    }
}
