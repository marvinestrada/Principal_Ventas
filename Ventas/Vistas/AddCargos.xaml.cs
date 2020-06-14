using System;
using System.Windows;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Input;

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
            if (Dfechita.Text != "" && txtcod_oper.Text != "" )
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

        private void TxtMonto_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal))
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        private void Txtcod_oper_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal))
            { e.Handled = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9))
            { e.Handled = false; }
            else
            { e.Handled = true; }
        }

        public void incluirDatos()
        {
            Dfechita.Text = Fecha;
            txtcod_oper.Text = Id_Op;
            txtMonto.Text = Monto;


        }

        public void actualizar()
        {
            try { 
                if (Dfechita.Text != "" && txtcod_oper.Text != "" && txtMonto.Text != "")
                {
                 SqlCommand cmd = new SqlCommand("spCargos", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CRUD", 3);
                    cmd.Parameters.AddWithValue("@Id_car", Idd.ToString());
                    cmd.Parameters.AddWithValue("@Fecha_cargo",Convert.ToString( Dfechita));
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
                if (Dfechita.Text != "" && txtcod_oper.Text != "" && txtMonto.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spCargos", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@crud", 1);
                    cmd.Parameters.AddWithValue("@Id_car", Idd.ToString());
                    cmd.Parameters.AddWithValue("@Fecha_cargo", Convert.ToString(Dfechita));
                    cmd.Parameters.AddWithValue("@Id_op", txtcod_oper.Text);
                    cmd.Parameters.AddWithValue("@Monto_cobro", txtMonto.Text);

                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();

                }

                
                  else MessageBox.Show("fallo");
                    
                
            }
            catch (Exception asdd)
            {
                asdd.ToString();
                Conexion.conex.Close();
            }

        }



    }
}
