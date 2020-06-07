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
    public partial class Pagos : Window
    {
        public Pagos()
        {
            InitializeComponent();
        }


        private void btnCerrar(object sender, RoutedEventArgs e) //Boton que cierra la ventana
        {
            this.Close();
        }

        private void mover(object sender, MouseButtonEventArgs e) //Permite el moviemiento de la ventana
        {
            this.DragMove();
        }
    }
}
