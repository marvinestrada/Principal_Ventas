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
    public partial class AddOpreracionesTi : Window
    {
        int Opcion, Ids;
        public string Descripcion;
        public AddOpreracionesTi(int opcion, int id = 0, string descrip = "")
        {
            InitializeComponent();
            Opcion = opcion;
            Ids = id;
            Descripcion = descrip;           
            
            if (Opcion == 2) incluirDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtDesceipcion.Text != ""  )
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
            txtDesceipcion.Text = Descripcion;
            txtDesceipcion.Focus();
  
        }

        public void actualizar()
        {
            try
            {

                if (txtDesceipcion.Text != "" )
                {
                    SqlCommand cmd = new SqlCommand("spTipoOperaciones", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Crud", 2);
                    cmd.Parameters.AddWithValue("@Id_Operaciones", Ids.ToString());
                    cmd.Parameters.AddWithValue("@Descripcion", txtDesceipcion.Text);                    
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();
                    MessageBox.Show("Id Operacion " + Ids + " Actualizado correctamente");


                }

                else
                {
                    MessageBox.Show("Los campos no deben quedar vacios.");
                    
                }
            }
            catch (Exception w)
            {
                w.ToString();
                Conexion.conex.Close();
            }

            
        }

        public void Guardar()
        {
            try
            {
                if (txtDesceipcion.Text != "" )
                {
                    SqlCommand cmd = new SqlCommand("spTipoOperaciones", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Crud", 0);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDesceipcion.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();

                }
                else MessageBox.Show("fallo");
            }
            catch(Exception asdd)
            {
                asdd.ToString();
                Conexion.conex.Close();
            }

                

            
        }

       
    }
}
