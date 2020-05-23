using System;
using System.Windows;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para AddCajas.xaml
    /// </summary>
    public partial class AddOperaciones : Window
    {
        int Opcion, Idd;
        public string Fecha, Id_Empleado, Id_TipoOperacion;
        public AddOperaciones(int opcion, int id = 0, string tipo_operacion = "", string fecha_de_operacion = "", string id_empleado = "")
        {
            InitializeComponent();
            Opcion = opcion;
            Idd = id;
            Id_TipoOperacion = tipo_operacion;
            Fecha = fecha_de_operacion;
            Id_Empleado = id_empleado;
        
            if (Opcion == 2) incluirDatos();
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void btnAceptar(object sender, RoutedEventArgs e)
        {
            if (Dfechita.Text != "" && txttipooperacion.Text != "" && txtIdempleado.Text != "")
            {
                if (Opcion == 1) { Guardar(); }
                else if (Opcion == 2) { actualizar(); }
            }
            else MessageBox.Show("Los campos no deben quedar vacios, no sea mula.");

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
            Dfechita.Text = Fecha;
            txtIdempleado.Text = Id_Empleado;
            txttipooperacion.Text = Id_TipoOperacion;


        }

        public void actualizar()
        {
            try { 
            if (Dfechita.Text != "" && txttipooperacion.Text != "" && txtIdempleado.Text != "")
            {
                SqlCommand cmd = new SqlCommand("spOperaciones", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@crud", 3);
                    cmd.Parameters.AddWithValue("@Id_operaciones", Idd.ToString());
                    cmd.Parameters.AddWithValue("@Id_tipo_oper", txttipooperacion.Text);
                    cmd.Parameters.AddWithValue("@Fecha_operac", Convert.ToString(Dfechita));
                    cmd.Parameters.AddWithValue("@Id_empleado", txtIdempleado.Text);


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
            
                if (Dfechita.Text != "" && txttipooperacion.Text != "" && txtIdempleado.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spOperaciones", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@crud", 1);
                    cmd.Parameters.AddWithValue("@Id_tipo_oper", txttipooperacion.Text);
                    cmd.Parameters.AddWithValue("@Fecha_operac", Convert.ToString(Dfechita.Text));
                    cmd.Parameters.AddWithValue("@Id_empleado", txtIdempleado.Text);

                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();

                }

                       else MessageBox.Show("fallo");

   




        }

       
    }
}
