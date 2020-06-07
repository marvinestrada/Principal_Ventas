using System.Windows;
using System.Windows.Input;


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

        private void mover(object sender, MouseButtonEventArgs e) //permite el moviemiento de la pantalla dentro del sistema
        {
            this.DragMove();
        }

        private void btnCerrar(object sender, RoutedEventArgs e)// cierra la ventana
        {
            this.Close();
        }

        private void Vent_reporte(object sender, RoutedEventArgs e) //crea conexion y muestra reporte realizado en el ide Crystal Report
        {
            ReporteVentas abrir = new ReporteVentas();
            abrir.Load(@"ReporteVentas.rep");
            VerDatos.ViewerCore.ReportSource = abrir;

        }
    }
}
