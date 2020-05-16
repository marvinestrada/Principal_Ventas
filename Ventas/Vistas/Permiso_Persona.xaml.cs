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
    public partial class Permiso_Persona : Window
    {
        public Permiso_Persona()
        {
            InitializeComponent();
            Conexiones();
            
        }

        public void Conexiones()
        {
              SqlCommand cmd = new SqlCommand("spPermisosEmpleado", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Crud", 2);
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
                string id_permiso = (vista["ID"]).ToString();
                string id_empleado = (vista)["Id Empleado"].ToString();
                int id_emple_per = (int)(vista)["Id Permiso Empleados"];
                AddPermiso_Personas abrir = new AddPermiso_Personas(2, id_emple_per, id_empleado, id_permiso);
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
                int result = (int)(vista["Permiso Empleado"]);
                MessageBoxResult respuesta = System.Windows.MessageBox.Show("Esta seguro de eliminar?",
                                            "confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (respuesta == MessageBoxResult.Yes)
                    {                        
                        SqlCommand cmd = new SqlCommand("spPermisosEmpleado", Conexion.conex);                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CRUD", 4);
                        cmd.Parameters.AddWithValue("@id_emple_per", result);
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
            AddPermiso_Personas mostrar = new AddPermiso_Personas(1);
            mostrar.ShowDialog();
            mostrar.Close();
            Conexiones();
            //estaba llamando a la tabla que agrega Permiso Personas 
        }
    }
}
