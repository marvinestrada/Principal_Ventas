﻿using System;
using System.Windows;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para AddCajas.xaml
    /// </summary>
    public partial class AddCajas : Window
    {
        int Opcion, Idd;
        public string Fecha, Id_Empleado, Comentarios, Monto;
        public AddCajas(int opcion, int id = 0, string fecha = "", string id_empleado = "", string comentarios = "", string monto = "")
        {
            InitializeComponent();
            Opcion = opcion;
            Idd = id;
            Fecha = fecha;
            Id_Empleado = id_empleado;
            Comentarios = comentarios;
            Monto = monto;

            if (Opcion == 2) incluirDatos();
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void btnAceptar(object sender, RoutedEventArgs e)
        {
            if (txtFecha.Text != "" && txtcod_empleado.Text != "" && txtComentarios.Text != "")
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
            txtcod_empleado.Text = Id_Empleado;
            txtComentarios.Text = Comentarios;
            txtMonto.Text = Monto;


        }

        public void actualizar()
        {

            if (txtFecha.Text != "" && txtcod_empleado.Text != "" && txtComentarios.Text != "")
            {
                SqlCommand cmd = new SqlCommand("spCaja", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@crud", 3);
                cmd.Parameters.AddWithValue("@Id_caja", Idd.ToString());
                cmd.Parameters.AddWithValue("@Fecha", txtFecha.Text);
                cmd.Parameters.AddWithValue("@Id_Empleado", txtcod_empleado.Text);
                cmd.Parameters.AddWithValue("@Comentarios", txtComentarios.Text);
                cmd.Parameters.AddWithValue("@Monto", Convert.ToDecimal(txtMonto.Text));

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
            
                if (txtFecha.Text != "" && txtcod_empleado.Text != "" && txtComentarios.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spCaja", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@crud", 1);
                    cmd.Parameters.AddWithValue("@Id_caja", Idd);
                    cmd.Parameters.AddWithValue("@Fecha",txtFecha.Text);
                    cmd.Parameters.AddWithValue("@Id_empleado", txtcod_empleado.Text);
                    cmd.Parameters.AddWithValue("@Comentarios", txtComentarios.Text);
                    cmd.Parameters.AddWithValue("@Monto", txtMonto.Text);

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
