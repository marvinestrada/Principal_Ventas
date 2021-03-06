﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using ProyectoTienda.Vistas;
using System.Windows.Forms;



namespace ProyectoTienda.Vistas
{
    /// <summary>
    /// Lógica de interacción para Personas.xaml
    /// </summary>
    public partial class Empleados : Window
    {
        public Empleados()
        {
            InitializeComponent();
            Conexiones();
        }

        public void Conexiones()
        {
           
                SqlCommand cmd = new SqlCommand("spEmpleado", Conexion.conex);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Crud", 2);
                cmd.Parameters.AddWithValue("@Crud", 2);
                DataTable tabla = new DataTable();
                Conexion.conex.Open();
                SqlDataAdapter puente = new SqlDataAdapter(cmd);
                puente.Fill(tabla);
                Conexion.conex.Close();
                ventana.DataContext = tabla;  
            
            
        }
        
        private void mover(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (ventana.SelectedCells.Count > 0)
            {
            try
            { 
                DataRowView vista = (DataRowView)ventana.SelectedItem;
                int id_persona = (int)(vista["Cod empleado"]);
                String nombres = (vista["Cod persona"]).ToString();
                String direcciones = (vista["Cod puesto"]).ToString();
                //int id_persona = (int)(vista["Id_Empleado"]);
                //String nombres = (vista["Id_persona"]).ToString();
                //String direcciones = (vista["Id_puesto"]).ToString();
                String telefonos = (vista["Alias"]).ToString();
                String empresas = (vista["Contraseña"]).ToString();
                AddEmpleados abrir = new AddEmpleados(2, id_persona, nombres, empresas, telefonos, direcciones);
                abrir.ShowDialog();
                abrir.Close();
                Conexiones();
            }
            catch (Exception b)
            {
                b.ToString();
                Conexion.conex.Close();
            }

        }
            else System.Windows.MessageBox.Show("Por favor seleccione algun dato de la tabla.");

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Cierra la Ventana
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Conexiones();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
            {
           
            if (ventana.SelectedCells.Count > 0)
            {
                DataRowView vista = (DataRowView)ventana.SelectedItem;

                int result = (int)(vista["Cod Empleado"]);

                //int result = (int)(vista["Codigo Empleado"]);
                

                    MessageBoxResult respuesta = System.Windows.MessageBox.Show("Esta seguro que desea eliminar?",
                                            "confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (respuesta == MessageBoxResult.Yes)
                    {                        
                        SqlCommand cmd = new SqlCommand("spEmpleado", Conexion.conex);                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Crud", 4);                        
                        cmd.Parameters.AddWithValue("@Id_empleado", result);                       
                        //cmd.Parameters.AddWithValue("@Crud", 4);                        
                        //cmd.Parameters.AddWithValue("@Id_Empleado", result);                       
                        Conexion.conex.Open();                
                        cmd.ExecuteNonQuery();           
                        Conexion.conex.Close();
                        Conexiones();
                        System.Windows.MessageBox.Show("Eliminado exitosamente");
                    }
           

        }
            else System.Windows.MessageBox.Show("Por favor seleccionar algun dato de la tabla");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddEmpleados mostrar = new AddEmpleados(1);
            mostrar.ShowDialog();
            mostrar.Close();
            Conexiones();

        }

        private void ventana_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
