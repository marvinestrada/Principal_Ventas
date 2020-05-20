using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para AddPersonas.xaml
    /// </summary>
    public partial class AddStock : Window
    {
        int Opcion, Ids, Id_operacion, Id_producto, Id_estado, Cantidad;
        
        decimal Precio;
        public AddStock(int opcion, int id = 0, int cantidad = 0, decimal precio = 0, int id_estado = 0, int id_operacion = 0, int id_producto = 0)
        {
            InitializeComponent();
            Opcion = opcion;
            Ids = id;
            Cantidad = cantidad;
            Precio = precio;
            Id_estado = id_estado;
            Id_operacion = id_operacion;
            Id_producto = id_producto;
            if (Opcion == 2) incluirDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "" && txtIdEstado.Text != "" && txtPrecio.Text != "" && txtIdOperaciones.Text != "" && txtIdProducto.Text != "" )
            {
                if (Opcion == 1) { Guardar(); }
                else if (Opcion == 2) { actualizar(); }
            }
            else MessageBox.Show("Los campos no deben quedar vacios.");

            
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
            Productos abrir = new Productos();
            abrir.enviarlo += new Productos.enviar(ejecutar);
            abrir.btnEnviarDato.Visibility = Visibility;
            abrir.btnActualizar.Visibility = Visibility.Hidden;
            abrir.btnBorrar.Visibility = Visibility.Hidden;
            abrir.btnInsertar.Visibility = Visibility.Hidden;
            abrir.btnRefrescar.Visibility = Visibility.Hidden;

            abrir.ShowDialog();
        }

        public void Cerrar()
        {
            this.Closing -= Window_Closing;
            base.Close();
        }

      

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal))
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back))
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void txtIdOperaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) )
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void txtIdProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) )
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void txtIdEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back))
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

       public void incluirDatos()
        {
            txtCantidad.Text = Cantidad.ToString();
            txtPrecio.Text = Precio.ToString();
            txtIdEstado.Text = Id_estado.ToString();
            txtIdOperaciones.Text = Id_operacion.ToString();
            txtIdProducto.Text = Id_producto.ToString();
            txtCantidad.Focus();

  
        }

        public void ejecutar (string dato)
        {
            txtIdProducto.Text = dato;
        }

        public void actualizar()
        {
           

                if (txtCantidad.Text != "" && txtIdEstado.Text != "" && txtPrecio.Text != "" && txtIdOperaciones.Text != "" && txtIdProducto.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spStock", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Opcion", 3);
                    cmd.Parameters.AddWithValue("@Id", Ids.ToString());
                    cmd.Parameters.AddWithValue("@Cantidad", txtCantidad.Text);
                    cmd.Parameters.AddWithValue("@IdEstado", txtIdEstado.Text);
                    cmd.Parameters.AddWithValue("@Precio", Convert.ToDecimal(txtPrecio.Text));
                    cmd.Parameters.AddWithValue("@IdProducto", txtIdProducto.Text);
                    cmd.Parameters.AddWithValue("@IdOperaciones", txtIdOperaciones.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();
                    MessageBox.Show("Id Stock " + Ids + " Actualizado correctamente");


                }

                else
                {
                    MessageBox.Show("Los campos no deben quedar vacios.");
                    
                }
                  
        }

        public void Guardar()
        {
            try { 
                if (txtCantidad.Text != "" && txtIdEstado.Text != "" && txtPrecio.Text != "" && txtIdOperaciones.Text != "" && txtIdProducto.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spStock", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Opcion", 1);
                    cmd.Parameters.AddWithValue("@Cantidad", txtCantidad.Text);
                    cmd.Parameters.AddWithValue("@IdEstado", txtIdEstado.Text);
                    cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                    cmd.Parameters.AddWithValue("@IdProducto", txtIdProducto.Text);
                    cmd.Parameters.AddWithValue("@IdOperaciones", txtIdOperaciones.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();

                }
                else MessageBox.Show("fallo");

             }
             catch (Exception w)
             {
                w.ToString();
                Conexion.conex.Close();
             }





        }


       
    }
}
