using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrimerParcial.Consultas
{
    public partial class cDepositos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        Expression<Func<Depositos, bool>> filter = x => true;

        private void Filtrar()
        {
            int dato = 0;
            switch (FiltroDropDownList.SelectedIndex)
            {
                case 0://Todo
                    filter = x => true;
                    break;

                case 1://DepositoId
                    dato = int.Parse(BuscarTextBox.Text);
                    filter = (x => x.DepositosId == dato);
                    break;

                case 2://CuentaId
                    dato = int.Parse(BuscarTextBox.Text);
                    filter = (x => x.CuentaId == dato);
                    break;

                case 3://Fecha
                    filter = (x => x.Fecha.Equals(BuscarTextBox.Text));
                    break;

                case 4://Concepto
                    filter = (x => x.Concepto.Contains(BuscarTextBox.Text));
                    break;

                case 5://Monto
                    double monto = double.Parse(BuscarTextBox.Text);
                    filter = (x => x.Monto == monto);
                    break;
            }
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Depositos> rep = new RepositorioBase<Depositos>();
            Filtrar();
            DepositoGridView.DataSource = rep.GetList(filter);
            DepositoGridView.DataBind();
        }

        protected void DepositoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RepositorioBase<Depositos> rep = new RepositorioBase<Depositos>();
            DepositoGridView.DataSource = rep.GetList(filter);
            DepositoGridView.PageIndex = e.NewPageIndex;
            DepositoGridView.DataBind();
        }

    }
}