using System;
using System.Windows;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para AddPersonas.xaml
    /// </summary>
    public partial class AddPersonas : Window
    {
        int Opcion, Ids;
        public string Nombre, Direccion, Telefono, Empresa;
        public AddPersonas(int opcion, int id = 0, string nombre = "", string empresa = "", string telefono = "", string direccion = "")
        {
            InitializeComponent();
            Opcion = opcion;
            Ids = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Empresa = empresa;
            if (Opcion == 2) incluirDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text != "" && txtEmpresa.Text != "" && txtDireccion.Text != "" && txtTelefono.Text != "")
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
        public void Cerrar()
        {
            this.Closing -= Window_Closing;
            base.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

       public void incluirDatos()
        {
            txtNombre.Text = Nombre;
            txtDireccion.Text = Direccion;
            txtTelefono.Text = Telefono;
            txtEmpresa.Text = Empresa;
            txtNombre.Focus();
  
        }

        public void actualizar()
        {
            try
            {
                if (txtNombre.Text != "" && txtEmpresa.Text != "" && txtDireccion.Text != "" && txtTelefono.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spPersonas", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Opcion", 3);
                    cmd.Parameters.AddWithValue("@Id", Ids.ToString());
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Empresa", txtEmpresa.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();
                    MessageBox.Show("Id "+Ids + " Actualizado exitosamente");
                }

                else
                {
                    MessageBox.Show("Los campos no deben quedar vacios.");
                    Conexion.conex.Close();
                }
            }
            catch (Exception b)
            {
                b.ToString();
                Conexion.conex.Close();
            }
         
        }

        public void Guardar()
        {
            try
            {


                if (txtNombre.Text != "" && txtEmpresa.Text != "" && txtDireccion.Text != "" && txtTelefono.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spPersonas", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Opcion", 1);
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Empresa", txtEmpresa.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();

                }

                else
                {
                    MessageBox.Show("Los campos no deben quedar vacios.");
                    Conexion.conex.Close();
                }
            }
            catch(Exception asda)
            {
                asda.ToString();
                Conexion.conex.Close();
            }


            
        }

       
    }
}
