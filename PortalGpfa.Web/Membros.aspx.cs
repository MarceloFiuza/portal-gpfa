using System;
using System.Collections.Generic;
using System.Web.UI;
using PortalGpfa.Web.Aplicacao;
using PortalGpfa.Web.Dominio;

namespace PortalGpfa.Web
{
    public partial class PaginaMembros : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarMembros();
            }
        }

        private void CarregarMembros()
        {
            ConsultasPortal consultas = new ConsultasPortal(SessaoPortal.Obter());
            IReadOnlyList<Membro> membros = consultas.ListarMembros();

            rptMembros.DataSource = membros;
            rptMembros.DataBind();

            rptMembros.Visible = membros.Count > 0;
            pnlVazio.Visible = membros.Count == 0;
        }
    }
}
