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
    public partial class AddPermiso_Personas : Window
    {
        int Opcion, Ids;
        public string Descripcion, Perm, Empl;  
       
        public AddPermiso_Personas(int opcion, int id = 0, string id_empleado = "", string id_emple_per = "", string descrip = "")
        {
            InitializeComponent();
            Opcion = opcion;
            Ids = id;
            Perm = id_empleado;
            Empl = id_emple_per;
            Descripcion = descrip;
            if (Opcion == 2) incluirDatos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cerrar();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtpermiso.Text != ""  )
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
            txtpermiso.Text = Descripcion;
           
            txtpermiso.Focus();
  
        }

        public void actualizar()
        {
            try
            {

                if (txtpermiso.Text != "" )
                {
                    SqlCommand cmd = new SqlCommand("spPermisosEmpleado", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CRUD", 3);
                    cmd.Parameters.AddWithValue("@id_emple_per", Ids.ToString());
                    cmd.Parameters.AddWithValue("@Id_empleado", txtempleado.Text);
                    cmd.Parameters.AddWithValue("@Id_permiso", txtpermiso.Text);
                    Conexion.conex.Open();
                    cmd.ExecuteNonQuery();
                    Conexion.conex.Close();
                    this.Close();
                    MessageBox.Show("Id Permiso " + Ids + " Actualizado correctamente");
                }
         //MI AMIGO.... :c
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

        private void button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        public void Guardar()
        {
            try
            {
                if (txtpermiso.Text != "" )
                {
                    SqlCommand cmd = new SqlCommand("spPermisosEmpleado", Conexion.conex);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CRUD", 1);
                    cmd.Parameters.AddWithValue("@Id_empleado", txtempleado.Text);
                    cmd.Parameters.AddWithValue("@Id_permiso", txtpermiso.Text);
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