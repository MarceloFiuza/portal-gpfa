using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalGpfa.Web.Aplicacao;
using PortalGpfa.Web.Dominio;

namespace PortalGpfa.Web
{
    public partial class PaginaPerfilMembro : Page
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
                    "O perfil solicitado não possui um identificador válido.");
                return;
            }

            ConsultasPortal consultas = new ConsultasPortal(SessaoPortal.Obter());
            Membro membro = consultas.BuscarMembro(id);

            if (membro == null)
            {
                MostrarMensagem(
                    "Membro não encontrado",
                    "Não há membro cadastrado com o identificador informado.");
                return;
            }

            PreencherPerfil(consultas, membro);
        }

        protected void rptArtigos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }

            HyperLink link = (HyperLink)e.Item.FindControl("lnkArtigo");
            Artigo artigo = e.Item.DataItem as Artigo;

            if (artigo == null || !Urls.EhHttp(artigo.Link))
            {
                link.Visible = false;
                return;
            }

            link.NavigateUrl = artigo.Link;
            link.Target = "_blank";
            link.Attributes["rel"] = "noopener noreferrer";
            link.Attributes["aria-label"] = "Acessar artigo: " + artigo.Titulo + " (abre em nova aba)";
        }

        private void PreencherPerfil(ConsultasPortal consultas, Membro membro)
        {
            pnlMensagem.Visible = false;
            pnlPerfil.Visible = true;
            Page.Title = membro.Nome + " — Portal GPFA";

            litTipo.Text = Rotulos.TipoMembro(membro);
            litNome.Text = membro.Nome;
            litFormacao.Text = membro.Formacao;
            litBiografia.Text = membro.Biografia;
            litPretensoes.Text = membro.PretensoesPesquisa;

            if (string.IsNullOrWhiteSpace(membro.FotoUrl))
            {
                imgFoto.Visible = false;
            }
            else
            {
                imgFoto.ImageUrl = ResolveUrl(membro.FotoUrl);
                imgFoto.AlternateText = "Avatar fictício de " + membro.Nome;
            }

            ConfigurarLink(lnkLattes, membro.LattesUrl, "Perfil Lattes");
            ConfigurarLink(lnkOrcid, membro.OrcidUrl, "Perfil ORCID");
            ConfigurarLink(lnkScholar, membro.ScholarUrl, "Perfil no Google Scholar");

            rptArtigos.DataSource = membro.Artigos;
            rptArtigos.DataBind();

            Pesquisador pesquisador = membro as Pesquisador;
            if (pesquisador != null)
            {
                PreencherRelacoesPesquisador(consultas, pesquisador);
                return;
            }

            Estudante estudante = membro as Estudante;
            if (estudante != null)
            {
                PreencherRelacoesEstudante(consultas, estudante);
                return;
            }

            pnlTecnico.Visible = membro is Tecnico;
        }

        private void PreencherRelacoesPesquisador(ConsultasPortal consultas, Pesquisador pesquisador)
        {
            pnlPesquisador.Visible = true;

            IReadOnlyList<Estudante> atuais = consultas.OrientandosAtuais(pesquisador);
            rptOrientandosAtuais.DataSource = atuais;
            rptOrientandosAtuais.DataBind();
            AjustarLista(rptOrientandosAtuais, pnlSemAtuais, atuais.Count);

            IReadOnlyList<Estudante> passados = consultas.OrientandosPassados(pesquisador);
            rptOrientandosPassados.DataSource = passados;
            rptOrientandosPassados.DataBind();
            AjustarLista(rptOrientandosPassados, pnlSemPassados, passados.Count);

            IReadOnlyList<Projeto> projetos = consultas.ProjetosDoPesquisador(pesquisador);
            rptProjetosPesquisador.DataSource = projetos;
            rptProjetosPesquisador.DataBind();
            AjustarLista(rptProjetosPesquisador, pnlSemProjetosPesquisador, projetos.Count);
        }

        private void PreencherRelacoesEstudante(ConsultasPortal consultas, Estudante estudante)
        {
            pnlEstudante.Visible = true;

            IReadOnlyList<VinculoOrientacao> orientacoes = consultas.OrientacoesDoEstudante(estudante);
            rptOrientacoes.DataSource = orientacoes;
            rptOrientacoes.DataBind();
            AjustarLista(rptOrientacoes, pnlSemOrientacoes, orientacoes.Count);

            IReadOnlyList<Coorientador> coorientadores = consultas.CoorientadoresDoEstudante(estudante);
            rptCoorientadores.DataSource = coorientadores;
            rptCoorientadores.DataBind();
            AjustarLista(rptCoorientadores, pnlSemCoorientadores, coorientadores.Count);

            IReadOnlyList<Projeto> projetos = consultas.ProjetosDoEstudante(estudante);
            rptProjetosEstudante.DataSource = projetos;
            rptProjetosEstudante.DataBind();
            AjustarLista(rptProjetosEstudante, pnlSemProjetosEstudante, projetos.Count);
        }

        private void ConfigurarLink(HyperLink link, string url, string descricao)
        {
            if (!Urls.EhHttp(url))
            {
                link.Visible = false;
                return;
            }

            link.NavigateUrl = url;
            link.Target = "_blank";
            link.Attributes["rel"] = "noopener noreferrer";
            link.Attributes["aria-label"] = descricao + " (abre em nova aba)";
        }

        private static void AjustarLista(Repeater lista, Panel vazio, int quantidade)
        {
            lista.Visible = quantidade > 0;
            vazio.Visible = quantidade == 0;
        }

        private void MostrarMensagem(string titulo, string texto)
        {
            pnlPerfil.Visible = false;
            pnlMensagem.Visible = true;
            litTituloMensagem.Text = titulo;
            litTextoMensagem.Text = texto;
            Page.Title = titulo + " — Portal GPFA";

            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
        }
    }
}
