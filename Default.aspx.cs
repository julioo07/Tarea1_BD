using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;



namespace TareaBD
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                CargaDatosEmpleado();
            }
        }
        public void CargaDatosEmpleado()  // encargado de Mostrar la lista de los empleados
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPSEmpleado"; // SP que usaremos
                cmd.Connection = conn;

                // Agrega el parámetro de salida
                SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                outParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParameter);

                conn.Open();
                cmd.ExecuteNonQuery();  // Se ejecuta el SP

                // Se ve el codigo que se obtuvo
                int resultado = (int)cmd.Parameters["@OutResulTCode"].Value;
                if (resultado == 0)
                {
                    // El código de resultado es exitoso, ahora obten los datos y asigna al GridView
                    using (SqlCommand cmdSelect = new SqlCommand("SELECT [Id], [Nombre], [Salario] FROM [BaseDatos].[dbo].[Empleado] ORDER BY Nombre", conn))
                    {
                        // ahora si se use el Execute Reader
                        using (SqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                gvdEmpleados.DataSource = reader;
                                gvdEmpleados.DataBind();
                            }
                        }
                    }
                } 
            }
        }


        public void InsertarEmpleado()
        {
            // Valida que hayan ingresado datos en el nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarMensaje("Error: Ingrese un nombre válido");
                return;
            }
            // Valida que no sean numeros
            if (System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, @"[^A-Za-z\s-\s-ÁÉÍÓÚáéíóú]"))
            {
                MostrarMensaje("Error: El nombre no puede contener números");
                return;
            }

            // Validación del salario
            decimal salario;
            if (!decimal.TryParse(txtSalario.Text.Trim(), out salario))
            {
                MostrarMensaje("Error: Ingrese un salario válido");
                return;
            }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPIEmpleado";  // nombre del stored procedure encargado de InsertEmpleados
                cmd.Connection= conn;

                // Validación del nombre
                cmd.Parameters.Add("@inNombre", SqlDbType.VarChar).Value = txtNombre.Text.Trim();
                cmd.Parameters.Add("@inSalario", SqlDbType.Money).Value = decimal.Parse(txtSalario.Text.Trim());
                SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                
                outParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParameter);

                conn.Open();
                cmd.ExecuteNonQuery();
                
                // Validacion de errores
                int error = (int)cmd.Parameters["@OutResulTCode"].Value;
                if (error == 0)
                {
                    MostrarMensaje("Inserción exitosa");

                }
                if (error == 50006)
                {
                  MostrarMensaje("Error: El empleado ya existe");

                }
            }
        }

      
        private void MostrarMensaje(string mensaje)
        {
            pnlDatosEmpleado.Visible = false;
            lblMensajeError.Text = mensaje;
            pnlError.Visible = true;
        }


        protected void btnInsertarEmpleado_Click(object sender, EventArgs e)
        {
            pnlDatosEmpleado.Visible = false;
            pnlAltaEmpleado.Visible=true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            pnlAltaEmpleado.Visible=false;
            InsertarEmpleado();
            CargaDatosEmpleado();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlAltaEmpleado.Visible=false;
            pnlError.Visible = false;
            pnlDatosEmpleado.Visible=true;
        }

    }
}