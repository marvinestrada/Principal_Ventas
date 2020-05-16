using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Data;



namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para Cargos.xaml
    /// </summary>
    public partial class Cargos : Window
    {
        public Cargos()
        {
            InitializeComponent();
            Conexiones();
            
        }

        public void Conexiones()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spCargos", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@crud", 2);
                DataTable tabla = new DataTable();
                Conexion.conex.Open();
                SqlDataAdapter puente = new SqlDataAdapter(cmd);
                puente.Fill(tabla);
                Conexion.conex.Close();
                Ventana.DataContext = tabla;
                
            }
            catch(Exception asd)
            {
                asd.ToString();
                Conexion.conex.Close();
            }
            
        }

        private void mover(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void btnActualizar(object sender, RoutedEventArgs e)
        {
            if (Ventana.SelectedCells.Count > 0)
            {
                DataRowView vista = (DataRowView)Ventana.SelectedItem;
                int id_car = (int)(vista["Id Cargo"]);
                string fechaa = (vista["Fecha"]).ToString();
                String id_oper = (vista["Codigo Operacion"]).ToString();
                String monto = (vista["Monto"]).ToString(); 


                AddCargos abrir = new AddCargos(2, id_car, fechaa, id_oper, monto);
                abrir.ShowDialog();
                abrir.Close();
                Conexiones();
            }
            else System.Windows.MessageBox.Show("Seleccione algun dato de la tabla.");

        }
        private void btnCerrar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

     

        private void btnRefresh(object sender, RoutedEventArgs e)
        {
            Conexiones();
        }

        private void btnDelete(object sender, RoutedEventArgs e)
            {

            if (Ventana.SelectedCells.Count > 0)
            {
                DataRowView vista = (DataRowView)Ventana.SelectedItem;
                int result = (int)(vista["Id cargo"]);
                try
                {
                    MessageBoxResult respuesta = System.Windows.MessageBox.Show("Esta seguro de eliminar?",
                                            "confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("spCargos", Conexion.conex);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Crud", 4);
                        cmd.Parameters.AddWithValue("@Id_Car", result);
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
            else System.Windows.MessageBox.Show("Seleccione algun dato de la tabla");
        }

        private void btnInser(object sender, RoutedEventArgs e)
        {
            AddCargos mostrar = new AddCargos(1);
            mostrar.ShowDialog();
            mostrar.Close();
            Conexiones();

        }
    }
}
