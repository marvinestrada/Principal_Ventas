using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Data;



namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para Cajas.xaml
    /// </summary>
    public partial class Operaciones : Window
    {
        public Operaciones()
        {
            InitializeComponent();
            Conexiones();
            
        }

        public void Conexiones()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spCaja", Conexion.conex);
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
                int id_caja = (int)(vista["Id Caja"]);
                string fechaa = (vista["Fecha"]).ToString();
                String id_empleado = (vista["Id Empleado"]).ToString();
                String comentario = (vista["Comentarios"]).ToString();
                String monto = (vista["Monto"]).ToString();
                

                AddCajas abrir = new AddCajas(2, id_caja, fechaa, id_empleado, comentario, monto);
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
                int result = (int)(vista["Id caja"]);
                try
                {
                    MessageBoxResult respuesta = System.Windows.MessageBox.Show("Esta seguro de eliminar?",
                                            "confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("spCaja", Conexion.conex);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Crud", 4);
                        cmd.Parameters.AddWithValue("@Id_Caja", result);
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
            AddCajas mostrar = new AddCajas(1);
            mostrar.ShowDialog();
            mostrar.Close();
            Conexiones();

        }
    }
}
