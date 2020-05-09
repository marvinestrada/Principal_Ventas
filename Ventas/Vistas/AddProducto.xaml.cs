﻿using System;
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
    public partial class AddProducto : Window
    {
        int Opcion, Ids, Unidad_minima;
        public string Descripcion;
        Decimal Precio_venta;
        public AddProducto(int opcion, int id = 0, string descrip = "", Decimal precio_ven = 0, int unidad_min = 0)
        {
            InitializeComponent();
            Opcion = opcion;
            Ids = id;
            Descripcion = descrip;
            Precio_venta = precio_ven;
            Unidad_minima = unidad_min;
            
            if (Opcion == 2) incluirDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtDesceipcion.Text != "" && txtPrecioVenta.Text != "" && txtUnidadMin.Text != "" )
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
            txtPrecioVenta.Text = Precio_venta.ToString();
            txtUnidadMin.Text = Unidad_minima.ToString();
            txtDesceipcion.Focus();
  
        }

        public void actualizar()
        {
            try
            {

                if (txtDesceipcion.Text != "" && txtPrecioVenta.Text != "" && txtUnidadMin.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spProductos", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Opcion", 3);
                    cmd.Parameters.AddWithValue("@Id", Ids.ToString());
                    cmd.Parameters.AddWithValue("@Descripcion", txtDesceipcion.Text);
                    cmd.Parameters.AddWithValue("@PrecioVenta", txtPrecioVenta.Text);
                    cmd.Parameters.AddWithValue("@UnidadMin", txtUnidadMin.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();
                    MessageBox.Show("Id Producto " + Ids + " Actualizado correctamente");


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
                if (txtDesceipcion.Text != "" && txtPrecioVenta.Text != "" && txtUnidadMin.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("spProductos", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Opcion", 1);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDesceipcion.Text);
                    cmd.Parameters.AddWithValue("@PrecioVenta", txtPrecioVenta.Text);
                    cmd.Parameters.AddWithValue("@UnidadMin", txtUnidadMin.Text);
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
