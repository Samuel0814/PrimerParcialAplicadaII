using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrimerParcial.Registros
{
    public partial class rDepositos : System.Web.UI.Page
    {
        private RepositorioBase<Depositos> XD = new RepositorioDepositos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LlenarComboBox();
        }

        private void LlenarComboBox()
        {
            RepositorioBase<CuentasBancarias> rb = new RepositorioBase<CuentasBancarias>();
            CuentaDropDownList.DataSource = rb.GetList(x => true);
            CuentaDropDownList.DataValueField = "CuentaId";
            CuentaDropDownList.DataTextField = "Nombre";
            CuentaDropDownList.DataBind();
            CuentaDropDownList.Items.Insert(0, new ListItem("", ""));
        }

        protected void ButtonNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            TextBoxDepositoID.Text = string.Empty;
            TextBoxFecha.Text = string.Empty;
            TextBoxConcepto.Text = string.Empty;
            TextBoxMonto.Text = string.Empty;
        }

        private Depositos LlenaClase()
        {
            int id;
            if (TextBoxDepositoID.Text == String.Empty)
                id = 0;
            else
                id = int.Parse(TextBoxDepositoID.Text);

            return new Depositos(
                id,
                DateTime.Parse(TextBoxFecha.Text),
                int.Parse(CuentaDropDownList.SelectedValue),
                TextBoxConcepto.Text,
                double.Parse(TextBoxMonto.Text)
            );
        }

        protected void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                RepositorioDepositos XD = new RepositorioDepositos();
                int.TryParse(TextBoxDepositoID.Text, out int id);
                if (id == 0)
                {
                    if (XD.Guardar(LlenaClase()))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Popup", "alert('Deposito Guardado')", true);
                    Limpiar();
                }
                else
                {
                    XD.Modificar(LlenaClase());
                    Limpiar();
                }
            }
        }

        protected void ButtonEliminar_Click(object sender, EventArgs e)
        {
            RepositorioDepositos XD = new RepositorioDepositos();
            Depositos deposito = XD.Buscar(int.Parse(TextBoxDepositoID.Text));

            if (deposito != null)
            {
                XD.Eliminar(int.Parse(TextBoxDepositoID.Text));
                Limpiar();
            }
        }

        private void LlenarCampos(Depositos deposito)
        {
            TextBoxFecha.Text = deposito.Fecha.ToString();
            CuentaDropDownList.SelectedValue = deposito.CuentaId.ToString();
            TextBoxConcepto.Text = deposito.Concepto;
            TextBoxMonto.Text = deposito.Monto.ToString();
        }

        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            RepositorioBase<Depositos> rb = new RepositorioBase<Depositos>();
            Depositos deposito = rb.Buscar(int.Parse(TextBoxDepositoID.Text));
            if (deposito != null)
                LlenarCampos(deposito);
        }
    }
}