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
        public void CargaDatosEmpleado()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPSEmpleados";  // Nombre del stored procedure encargado de EnseñarEmpleados
                cmd.Connection = conn;
                conn.Open();
                gvdEmpleados.DataSource = cmd.ExecuteReader();
                gvdEmpleados.DataBind();
            }
        }

        public void InsertarEmpleado()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPIEmpleado";  // nombre del stored procedure encargado de InsertEmpleados
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txtNombre.Text.Trim();
                cmd.Parameters.Add("@Salario", SqlDbType.Money).Value = decimal.Parse(txtSalario.Text.Trim());
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        protected void btnInsertarEmpleado_Click(object sender, EventArgs e)
        {
            pnlDatosEmpleado.Visible = false;
            pnlAltaEmpleado.Visible=true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            pnlAltaEmpleado.Visible=false;
            pnlDatosEmpleado.Visible=true;
            
            //lblMensaje.Text = "Inserción exitosa";
            //lblMensaje.Visible = true;
            InsertarEmpleado();
            CargaDatosEmpleado();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlAltaEmpleado.Visible=false;
            pnlDatosEmpleado.Visible=true;
        }
    }
}