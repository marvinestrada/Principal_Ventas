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
using System.ComponentModel;
using System.Configuration;
using ProyectoTienda.Vistas;
using System.Windows.Forms;



namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para Personas.xaml
    /// </summary>
    public partial class Empleados : Window
    {
        public Empleados()
        {
            InitializeComponent();
            Conexiones();
            
        }

        public void Conexiones()
        {
            
                SqlCommand cmd = new SqlCommand("spEmpleado", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Crud", 2);
                DataTable tabla = new DataTable();
                Conexion.conex.Open();
                SqlDataAdapter puente = new SqlDataAdapter(cmd);
                puente.Fill(tabla);
                Conexion.conex.Close();
                ventana.DataContext = tabla;           
        }

        private void mover(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ventana.SelectedCells.Count > 0)
            {
                DataRowView vista = (DataRowView)ventana.SelectedItem;
                int id_persona = (int)(vista["Codigo empleado"]);
                String nombres = (vista["Codigo persona"]).ToString();
                String direcciones = (vista["Codigo puesto"]).ToString();
                String telefonos = (vista["Alias"]).ToString();
                String empresas = (vista["Contraseña"]).ToString();
                AddPersonas abrir = new AddPersonas(2, id_persona, nombres, empresas, telefonos, direcciones);
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
           
            if (ventana.SelectedCells.Count > 0)
            {
                DataRowView vista = (DataRowView)ventana.SelectedItem;
                int result = (int)(vista["Codigo Empleado"]);
                try
                {
                    MessageBoxResult respuesta = System.Windows.MessageBox.Show("Esta seguro que desea eliminar?",
                                            "confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (respuesta == MessageBoxResult.Yes)
                    {                        
                        SqlCommand cmd = new SqlCommand("spEmpleado", Conexion.conex);                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Crud", 4);                        
                        cmd.Parameters.AddWithValue("@Id_Empleado", result);                       
                        Conexion.conex.Open();                
                        cmd.ExecuteNonQuery();           
                        Conexion.conex.Close();
                        Conexiones();
                        System.Windows.MessageBox.Show("Eliminado exitosamente");
                    }
                }
                catch (Exception ea)
                {
                    ea.ToString();
                    Conexion.conex.Close();
                }
            }
            else System.Windows.MessageBox.Show("Por favor seleccione algun dato de la tabla");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddEmpleados mostrar = new AddEmpleados(1);
            mostrar.ShowDialog();
            mostrar.Close();
            Conexiones();

        }
    }
}
