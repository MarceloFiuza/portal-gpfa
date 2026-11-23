<%@ Page Title="Projeto — Portal GPFA" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjetoDetalhe.aspx.cs" Inherits="PortalGpfa.Web.PaginaProjetoDetalhe" %>
<%@ Import Namespace="PortalGpfa.Web" %>
<%@ Import Namespace="PortalGpfa.Web.Dominio" %>
<asp:Content ID="ConteudoPrincipal" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlMensagem" runat="server" Visible="false" CssClass="estado-vazio">
        <h1><asp:Literal ID="litTituloMensagem" runat="server" Mode="Encode" /></h1>
        <p><asp:Literal ID="litTextoMensagem" runat="server" Mode="Encode" /></p>
        <div class="acoes">
            <asp:HyperLink ID="lnkVoltarProjetos" runat="server" CssClass="botao" NavigateUrl="~/Projetos.aspx">Voltar aos projetos</asp:HyperLink>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlProjeto" runat="server" Visible="false">
        <header class="cabecalho-pagina">
            <p class="etiqueta-situacao"><asp:Literal ID="litSituacao" runat="server" Mode="Encode" /></p>
            <h1><asp:Literal ID="litTitulo" runat="server" Mode="Encode" /></h1>
            <p class="nota-dados">Projeto fictício criado para demonstração acadêmica.</p>
        </header>

        <section aria-labelledby="descricao-projeto-titulo">
            <h2 id="descricao-projeto-titulo">Descrição</h2>
            <p><asp:Literal ID="litDescricao" runat="server" Mode="Encode" /></p>
        </section>

        <section aria-labelledby="dados-projeto-titulo">
            <h2 id="dados-projeto-titulo">Dados do projeto</h2>
            <dl class="dados-projeto">
                <div>
                    <dt>Coordenador</dt>
                    <dd><asp:HyperLink ID="lnkCoordenador" runat="server" /></dd>
                </div>
                <div>
                    <dt>Financiamento</dt>
                    <dd><asp:Literal ID="litVerba" runat="server" Mode="Encode" /></dd>
                </div>
            </dl>
            <p class="nota-dados">Valor fictício no nível do projeto; não representa rateio por pesquisador.</p>
        </section>

        <section aria-labelledby="pesquisadores-projeto-titulo">
            <h2 id="pesquisadores-projeto-titulo">Pesquisadores participantes</h2>
            <asp:Repeater ID="rptPesquisadores" runat="server">
                <HeaderTemplate>
                    <ul class="lista-relacoes">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="lnkPesquisador" runat="server" NavigateUrl='<%# "~/PerfilMembro.aspx?id=" + Eval("Id") %>'><%#: Eval("Nome") %></asp:HyperLink>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Panel ID="pnlSemPesquisadores" runat="server" Visible="false" CssClass="texto-vazio">
                <p>Nenhum pesquisador participante registrado.</p>
            </asp:Panel>
        </section>

        <section aria-labelledby="estudantes-projeto-titulo">
            <h2 id="estudantes-projeto-titulo">Estudantes vinculados</h2>
            <asp:Repeater ID="rptEstudantes" runat="server">
                <HeaderTemplate>
                    <ul class="lista-relacoes">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="lnkEstudante" runat="server" NavigateUrl='<%# "~/PerfilMembro.aspx?id=" + Eval("Id") %>'><%#: Eval("Nome") %></asp:HyperLink>
                        <span class="etiqueta"><%#: Rotulos.TipoMembro((Membro)Container.DataItem) %></span>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Panel ID="pnlSemEstudantes" runat="server" Visible="false" CssClass="texto-vazio">
                <p>Nenhum estudante vinculado registrado.</p>
            </asp:Panel>
        </section>

        <p><a href='<%= ResolveUrl("~/Projetos.aspx") %>' class="link-acao">Voltar à lista de projetos</a></p>
    </asp:Panel>
</asp:Content>
