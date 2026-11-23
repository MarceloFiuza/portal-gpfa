using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalGpfa.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litAno.Text = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
            DestacarPaginaAtual();
        }

        private void DestacarPaginaAtual()
        {
            string paginaAtual = VirtualPathUtility.GetFileName(Request.AppRelativeCurrentExecutionFilePath);

            if (string.Equals(paginaAtual, "Default.aspx", StringComparison.OrdinalIgnoreCase))
            {
                MarcarLinkAtivo(lnkInicio);
            }
            else if (string.Equals(paginaAtual, "Membros.aspx", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(paginaAtual, "PerfilMembro.aspx", StringComparison.OrdinalIgnoreCase))
            {
                MarcarLinkAtivo(lnkMembros);
            }
            else if (string.Equals(paginaAtual, "Projetos.aspx", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(paginaAtual, "ProjetoDetalhe.aspx", StringComparison.OrdinalIgnoreCase))
            {
                MarcarLinkAtivo(lnkProjetos);
            }
        }

        private static void MarcarLinkAtivo(HyperLink link)
        {
            link.CssClass = "ativo";
            link.Attributes["aria-current"] = "page";
        }
    }
}
