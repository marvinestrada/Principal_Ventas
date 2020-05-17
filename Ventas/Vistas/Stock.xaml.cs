using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Data;
using System.Configuration;
using ProyectoTienda.Vistas;
using System.Windows.Forms;



namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para Personas.xaml
    /// </summary>
    public partial class Stock : Window
    {
        public Stock()
        {
            InitializeComponent();
            Conexiones();
            
        }

        public void Conexiones()
        {
              SqlCommand cmd = new SqlCommand("spStock", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Opcion", 2);
                DataTable productoss = new DataTable();
                Conexion.conex.Open();
                SqlDataAdapter puente = new SqlDataAdapter(cmd);
                puente.Fill(productoss);
                Conexion.conex.Close();
                mostrarDatos.DataContext = productoss;
                
           //Establece la conexion con el procedimiento almacenado
            
        }

        private void mover(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mostrarDatos.SelectedCells.Count > 0)
            {
                DataRowView vista = (DataRowView)mostrarDatos.SelectedItem;
                int idStock = (int)(vista["Id"]);
                int cantidad = (int)(vista["Cantidad"]);
                decimal precio = (decimal)(vista["Precio"]);
                int idOperaciones = (int)(vista["Id Operaciones"]);
                int idProducto = (int)(vista["Id Producto"]);
                int idEstado = (int)(vista["Id Estado"]);


                AddStock abrir = new AddStock(2, idStock,cantidad, precio, idEstado,idOperaciones, idProducto );
                abrir.ShowDialog();
                abrir.Close();
                Conexiones();
            }
            else System.Windows.MessageBox.Show("Seleccione algun dato de la tabla.");

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

     

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Conexiones();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
            {

            

            if (mostrarDatos.SelectedCells.Count > 0)
            {
                DataRowView vista = (DataRowView)mostrarDatos.SelectedItem;
                int result = (int)(vista["Id"]);

                MessageBoxResult respuesta = System.Windows.MessageBox.Show("Esta seguro de eliminar?",
                                            "confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (respuesta == MessageBoxResult.Yes)
                    {                        
                        SqlCommand cmd = new SqlCommand("spStock", Conexion.conex);                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Opcion", 4);                        
                        cmd.Parameters.AddWithValue("@Id", result);                       
                        Conexion.conex.Open();                
                        cmd.ExecuteNonQuery();           
                        Conexion.conex.Close();
                        Conexiones();
                    }
                
               
            }
            else System.Windows.MessageBox.Show("Seleccione algun dato de la tabla");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddStock mostrar = new AddStock(1);
            mostrar.ShowDialog();
            mostrar.Close();
            Conexiones();

        }
    }
}
