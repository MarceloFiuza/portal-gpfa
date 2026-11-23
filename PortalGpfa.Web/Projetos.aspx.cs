using System;
using System.Collections.Generic;
using System.Web.UI;
using PortalGpfa.Web.Aplicacao;
using PortalGpfa.Web.Dominio;

namespace PortalGpfa.Web
{
    public partial class PaginaProjetos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarProjetos();
            }
        }

        private void CarregarProjetos()
        {
            ConsultasPortal consultas = new ConsultasPortal(SessaoPortal.Obter());
            IReadOnlyList<Projeto> projetos = consultas.ListarProjetos();

            rptProjetos.DataSource = projetos;
            rptProjetos.DataBind();

            rptProjetos.Visible = projetos.Count > 0;
            pnlVazio.Visible = projetos.Count == 0;
        }
    }
}
