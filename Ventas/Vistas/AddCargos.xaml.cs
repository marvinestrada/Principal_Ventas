using System;
using System.Windows;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para AddCargos.xaml
    /// </summary>
    public partial class AddCargos : Window
    {
        int Opcion, Idd;
        public string Fecha, Id_Op, Monto;
        public AddCargos(int opcion, int id = 0, string fecha = "", string id_op = "",  string monto = "")
        {
            InitializeComponent();
            Opcion = opcion;
            Idd = id;
            Fecha = fecha;
            Id_Op = id_op;
            Monto = monto;

            if (Opcion == 2) incluirDatos();
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void btnAceptar(object sender, RoutedEventArgs e)
        {
            if (txtFecha.Text != "" && txtcod_oper.Text != "" )
            {
                if (Opcion == 1) { Guardar(); }
                else if (Opcion == 2) { actualizar(); }
            }
            else MessageBox.Show("Los campos no deben quedar vacios.");

            this.Close();
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

        private void btnCerrar(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        public void incluirDatos()
        {
            txtFecha.Text = Fecha;
            txtcod_oper.Text = Id_Op;
            txtMonto.Text = Monto;


        }

        public void actualizar()
        {

            if (txtFecha.Text != "" && txtcod_oper.Text != "" && txtMonto.Text != "")
            {
                SqlCommand cmd = new SqlCommand("spCargos", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Crud", 3);
                cmd.Parameters.AddWithValue("@Id_car", Idd.ToString());
                cmd.Parameters.AddWithValue("@Fecha_cargo",txtFecha.Text);
                cmd.Parameters.AddWithValue("@Id_op", txtcod_oper.Text);
                cmd.Parameters.AddWithValue("@Monto_cobro", Convert.ToDecimal(txtMonto.Text));

                Conexion.conex.Open();
                cmd.ExecuteNonQuery();
                Conexion.conex.Close();


            }

            else
            {
                MessageBox.Show("Los campos no deben quedar vacios.");
                Conexion.conex.Close();
            }

        }
    
        

        public void Guardar()
        {
            
                if (txtFecha.Text != "" && txtcod_oper.Text != "" )
                {
                    SqlCommand cmd = new SqlCommand("spCargos", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@crud", 1);
                    cmd.Parameters.AddWithValue("@Id_car", Idd.ToString());
                    cmd.Parameters.AddWithValue("@Fecha_cargo", txtFecha.Text);
                    cmd.Parameters.AddWithValue("@Id_op", txtcod_oper.Text);
                    cmd.Parameters.AddWithValue("@Monto_cobro", txtMonto.Text);

                Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    
                }

                else
                {
                    MessageBox.Show("Los campos no deben quedar vacios.");
                    Conexion.conex.Close();
                }

            
            

               
           
        }

       
    }
}
