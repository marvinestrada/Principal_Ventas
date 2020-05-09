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
    public partial class OperacionesTi : Window
    {
        public OperacionesTi()
        {
            InitializeComponent();
            Conexiones();
            
        }

        public void Conexiones()
        {
              SqlCommand cmd = new SqlCommand("spTipoOperaciones", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Crud", 1);
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
                int id_tipo= (int)(vista["Id"]);
                String Descripcion = (vista["Descripcion"]).ToString();
                AddOpreracionesTi abrir = new AddOpreracionesTi(2, id_tipo, Descripcion);
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
                        SqlCommand cmd = new SqlCommand("spTipoOperaciones", Conexion.conex);                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Crud", 3);                        
                        cmd.Parameters.AddWithValue("@Id_Operaciones", result);                       
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
            AddOpreracionesTi mostrar = new AddOpreracionesTi(1);
            mostrar.ShowDialog();
            mostrar.Close();
            Conexiones();

        }

     
    }
}
