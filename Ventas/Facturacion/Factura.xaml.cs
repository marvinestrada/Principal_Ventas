using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProyectoTienda.Vistas;

namespace ProyectoTienda.Facturacion
{
    /// <summary>
    /// Lógica de interacción para Factura.xaml
    /// </summary>
    public partial class Factura : Window
    {
        public Factura()
        {
            InitializeComponent();
            DataContext = this;
            txtFecha.Text = DateTime.Now.ToString();
            
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtNit_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //busca el nit dentro de la base de datos
            if(e.Key == Key.Enter)
            {
                Conexiones();
                SqlCommand cmd = new SqlCommand("spPersonas", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opcion", 6);
                cmd.Parameters.AddWithValue("@Nit", txtNit.Text);
                Conexion.conex.Open();
                SqlDataReader identificar = cmd.ExecuteReader();
                Conexion.conex.Close();
                      

            }
        }

        public void Conexiones()
        {
            //hace la conexion para verificar si el nit se encuentra en la base de datos
            SqlCommand cmd = new SqlCommand("spPersonas", Conexion.conex);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Opcion", 5);
            cmd.Parameters.AddWithValue("@Nit", txtNit.Text);
            Conexion.conex.Open();
            SqlDataReader leer = cmd.ExecuteReader();

            if(leer.Read() == true)
            {
                txtNombre.Text = leer["Nombre"].ToString();
                txtDireccion.Text = leer["Direccion"].ToString();
                txtNit.Text = leer["Nit"].ToString();
                String Id = leer["Id_persona"].ToString();
                Conexion.conex.Close();
                leer.Close();

            }
            else
            {
                System.Windows.MessageBox.Show("Nit no registrado en la base de datos.");
                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtNit.Text = "";
                Conexion.conex.Close();
                leer.Close();
            }
            Conexion.conex.Close();
            leer.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddPersonas abrir = new AddPersonas(1);
            abrir.ShowDialog();

        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            //muestra la ventana de productos solamente con el boton especificado
            Productos abrir = new Productos();
            abrir.enviarFactura += new Productos.enviarproducto(ejecutar);
            abrir.btnEnviarDato.Visibility = Visibility.Hidden;
            abrir.btnActualizar.Visibility = Visibility.Hidden;
            abrir.btnBorrar.Visibility = Visibility.Hidden;
            abrir.btnInsertar.Visibility = Visibility.Hidden;
            abrir.btnRefrescar.Visibility = Visibility.Hidden;
            abrir.btnAddProducto.Visibility = Visibility;
            abrir.ShowDialog();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public static explicit operator Factura(Form v)
        {
            throw new NotImplementedException();
        }

        public void ejecutar(string codigo, string descripcion, decimal precioVenta, string Id_producto)
        {
            //creacion de columnas dentro del dataGrid
            DataTable dt = new DataTable();

            int cantidades = 1;
            decimal precio_venta_prod = (decimal)precioVenta;
            DataRow prim = dt.NewRow();
            DataColumn Codigos = new DataColumn("Codigo", typeof(string));
            DataColumn Descripcion = new DataColumn("Descripcion", typeof(string));
            DataColumn Cantidad = new DataColumn("Cantidad", typeof(string));
            DataColumn Precio_Venta = new DataColumn("Precio de Venta", typeof(string));
            DataColumn Total_venta = new DataColumn("Total", typeof(string));

            dt.Columns.Add(Codigos);
            dt.Columns.Add(Descripcion);
            dt.Columns.Add(Cantidad);
            dt.Columns.Add(Precio_Venta);
            dt.Columns.Add(Total_venta);



            decimal Totalventa = cantidades * precio_venta_prod;
            prim[0] = codigo;
            prim[1] = descripcion;
            prim[2] = cantidades;
            prim[3] = precioVenta;
            prim[4] = Totalventa;

            TotalProducto.Content = Totalventa.ToString();


            dt.Rows.Add(prim);

            ventana.ItemsSource = dt.DefaultView;

        }

    }
}
