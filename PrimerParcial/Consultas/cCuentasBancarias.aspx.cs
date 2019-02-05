﻿using BLL;
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
    public partial class cCuentasBancarias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        Expression<Func<CuentasBancarias, bool>> filtro = x => true;

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<CuentasBancarias> rep = new RepositorioBase<CuentasBancarias>();
            int dato = 0;
            switch (DropDownListFiltro.SelectedIndex)
            {
                case 0://Todo
                    filtro = x => true;
                    break;

                case 1://CuentaId
                    dato = int.Parse(TextBoxBuscar.Text);
                    filtro = (x => x.CuentaId == dato);
                    break;

                case 2://Fecha
                    filtro = (x => x.Fecha.Equals(TextBoxBuscar.Text));
                    break;

                case 3://Nombre
                    filtro = (x => x.Nombre.Contains(TextBoxBuscar.Text));
                    break;

                case 4://Balance
                    double balance = double.Parse(TextBoxBuscar.Text);
                    filtro = (x => x.Balance == balance);
                    break;
            }
            CuentaGridView.DataSource = rep.GetList(filtro);
            CuentaGridView.DataBind();
        }

        protected void CuentaGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RepositorioBase<CuentasBancarias> rb = new RepositorioBase<CuentasBancarias>();
            CuentaGridView.DataSource = rb.GetList(filtro);
            CuentaGridView.PageIndex = e.NewPageIndex;
            CuentaGridView.DataBind();
        }

        protected void CuentaGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}