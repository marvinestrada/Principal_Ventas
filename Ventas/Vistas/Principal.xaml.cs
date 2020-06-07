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

namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Operaciones JalarVetana = new Operaciones();
            JalarVetana.Owner = this;
            JalarVetana.ShowDialog();

        }

        private void btnOperaciones(object sender, RoutedEventArgs e)
        {
            Operaciones JalarVetana = new Operaciones();
            JalarVetana.Owner = this;
            JalarVetana.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnCerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCatalogos(object sender, RoutedEventArgs e)
        {
            Stock JalarVetana1 = new Stock();
            JalarVetana1.Owner = this;
            JalarVetana1.ShowDialog();
        }
    }
}
