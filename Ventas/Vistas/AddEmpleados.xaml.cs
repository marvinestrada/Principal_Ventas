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
    public partial class AddEmpleados : Window
    {
        int Opcion, Ids;
        public string CodPerso, CodPuest, Alias, Pass;
        public AddEmpleados(int opcion, int id = 0, string codperso = "", string codpuesto = "", string alias = "", string pass = "")
        {
            InitializeComponent();
            Opcion = opcion;
            Ids = id;
            CodPerso = codperso;
            CodPuest = codpuesto;
            Alias = alias;
            Pass = pass;
            if (Opcion == 2) incluirDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtCodper.Text != "" && txtCodpuesto.Text != "" && txtAlias.Text != "" && txtContra.Text != "")
            {
                if (Opcion == 1) { Guardar(); }
                else if (Opcion == 2) { actualizar(); }
            }
            else MessageBox.Show("Los campos no pueden estar vacios.");  
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
            txtCodper.Text = CodPerso;
            txtCodpuesto.Text = CodPuest;
            txtAlias.Text = Alias;
            txtContra.Text =Pass;
            txtCodper.Focus();
  
        }

        public void actualizar()
        {
            try
            {
                if (txtCodper.Text != "" && txtCodpuesto.Text != "" && txtAlias.Text != "" && txtContra.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spEmpleado", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Crud", 3);
                    cmd.Parameters.AddWithValue("@Id", Ids.ToString());
                    cmd.Parameters.AddWithValue("@Id_persona", txtCodper.Text);
                    cmd.Parameters.AddWithValue("@Id_puesto", txtCodpuesto.Text);
                    cmd.Parameters.AddWithValue("@Alias", txtAlias.Text);
                    cmd.Parameters.AddWithValue("@Pass", txtContra.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();
                    MessageBox.Show("Id "+Ids + " Actualizado exitosamente");
                }

                else
                {
                    MessageBox.Show("Los campos no pueden estar vacios.");
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


                if (txtCodper.Text != "" && txtCodpuesto.Text != "" && txtAlias.Text != "" && txtContra.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spEmpleados", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Crud", 1);
                    cmd.Parameters.AddWithValue("@Id_persona", txtCodper.Text);
                    cmd.Parameters.AddWithValue("@Id_puesto", txtCodpuesto.Text);
                    cmd.Parameters.AddWithValue("@Alias", txtAlias.Text);
                    cmd.Parameters.AddWithValue("@Pass", txtContra.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();

                }

                else
                {
                    MessageBox.Show("Los campos no pueden estar vacios.");
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
