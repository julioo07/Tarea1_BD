<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TareaBD._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <div class="row">
            <asp:Panel runat ="server" ID="pnlDatosEmpleado">

                Lista de Empleados
                <br/>
                <br/>

        <asp:GridView ID="gvdEmpleados" runat="server" AutoGenerateColumns="true">


        </asp:GridView>
                <br/>
                <br/>
                <asp:button ID="btnInsertarEmpleado" Text="Insertar Empleado" runat="server" OnClick="btnInsertarEmpleado_Click" />
            </asp:Panel>


            <asp:Panel ID="pnlAltaEmpleado" runat="server" Visible="false">
                <div> 
                    <br/>
                    <asp:Label ID="lblNombre" Text="Nombre: " runat="server"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" />
                </div>

                <div>
                    <br/>
                    <asp:Label ID="Salario" Text="Salario:" runat="server"></asp:Label>
                    <asp:TextBox ID="txtSalario" runat="server" />
                </div>
                <br/>
                <br/>
                    <asp:button ID="btnGuardar" runat="server" Text="Insertar" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
            </asp:Panel>

            <br/>
            


        </div>
    </main>

</asp:Content>
