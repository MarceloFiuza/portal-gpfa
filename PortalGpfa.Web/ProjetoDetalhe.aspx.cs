using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalGpfa.Web.Aplicacao;
using PortalGpfa.Web.Dominio;

namespace PortalGpfa.Web
{
    public partial class PaginaProjetoDetalhe : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            int id;
            if (!int.TryParse(Request.QueryString["id"], out id) || id <= 0)
            {
                MostrarMensagem(
                    "Identificador inválido",
                    "O projeto solicitado não possui um identificador válido.");
                return;
            }

            ConsultasPortal consultas = new ConsultasPortal(SessaoPortal.Obter());
            Projeto projeto = consultas.BuscarProjeto(id);

            if (projeto == null)
            {
                MostrarMensagem(
                    "Projeto não encontrado",
                    "Não há projeto cadastrado com o identificador informado.");
                return;
            }

            PreencherProjeto(consultas, projeto);
        }

        private void PreencherProjeto(ConsultasPortal consultas, Projeto projeto)
        {
            pnlMensagem.Visible = false;
            pnlProjeto.Visible = true;
            Page.Title = projeto.Titulo + " — Portal GPFA";

            litTitulo.Text = projeto.Titulo;
            litDescricao.Text = projeto.Descricao;
            litSituacao.Text = Rotulos.SituacaoProjeto(projeto.Situacao);

            lnkCoordenador.Text = Server.HtmlEncode(projeto.Coordenador.Nome);
            lnkCoordenador.NavigateUrl = "~/PerfilMembro.aspx?id=" + projeto.Coordenador.Id;

            litVerba.Text = projeto.VerbaCaptada.HasValue
                ? projeto.VerbaCaptada.Value.ToString("C2", new CultureInfo("pt-BR"))
                : "Financiamento não informado";

            IReadOnlyList<Pesquisador> pesquisadores = consultas.PesquisadoresDoProjeto(projeto);
            rptPesquisadores.DataSource = pesquisadores;
            rptPesquisadores.DataBind();
            AjustarLista(rptPesquisadores, pnlSemPesquisadores, pesquisadores.Count);

            IReadOnlyList<Estudante> estudantes = consultas.EstudantesDoProjeto(projeto);
            rptEstudantes.DataSource = estudantes;
            rptEstudantes.DataBind();
            AjustarLista(rptEstudantes, pnlSemEstudantes, estudantes.Count);
        }

        private static void AjustarLista(Repeater lista, Panel vazio, int quantidade)
        {
            lista.Visible = quantidade > 0;
            vazio.Visible = quantidade == 0;
        }

        private void MostrarMensagem(string titulo, string texto)
        {
            pnlProjeto.Visible = false;
            pnlMensagem.Visible = true;
            litTituloMensagem.Text = titulo;
            litTextoMensagem.Text = texto;
            Page.Title = titulo + " — Portal GPFA";

            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
        }
    }
}
