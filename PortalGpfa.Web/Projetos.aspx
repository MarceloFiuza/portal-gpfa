<%@ Page Title="Projetos — Portal GPFA" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projetos.aspx.cs" Inherits="PortalGpfa.Web.PaginaProjetos" %>
<%@ Import Namespace="PortalGpfa.Web" %>
<%@ Import Namespace="PortalGpfa.Web.Dominio" %>
<asp:Content ID="ConteudoPrincipal" ContentPlaceHolderID="MainContent" runat="server">
    <header class="cabecalho-pagina">
        <h1>Projetos</h1>
        <p>Projetos de pesquisa do GPFA em andamento e concluídos.</p>
        <p class="nota-dados">Todos os projetos, valores e participantes desta demonstração são fictícios.</p>
    </header>

    <div class="grade-projetos">
        <asp:Repeater ID="rptProjetos" runat="server">
            <ItemTemplate>
                <article class="cartao-projeto">
                    <p class="etiqueta-situacao"><%#: Rotulos.SituacaoProjeto(((Projeto)Container.DataItem).Situacao) %></p>
                    <h2>
                        <asp:HyperLink ID="lnkDetalhe" runat="server" NavigateUrl='<%# "~/ProjetoDetalhe.aspx?id=" + Eval("Id") %>'><%#: Eval("Titulo") %></asp:HyperLink>
                    </h2>
                    <p class="descricao-cartao"><%#: Eval("Descricao") %></p>
                    <p>
                        <asp:HyperLink ID="lnkAbrir" runat="server" CssClass="link-acao" NavigateUrl='<%# "~/ProjetoDetalhe.aspx?id=" + Eval("Id") %>' aria-label='<%#: "Ver detalhes do projeto " + Eval("Titulo") %>'>Ver detalhes</asp:HyperLink>
                    </p>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:Panel ID="pnlVazio" runat="server" Visible="false" CssClass="estado-vazio">
        <h2>Nenhum projeto cadastrado</h2>
        <p>A área de projetos está vazia no estado atual da sessão.</p>
    </asp:Panel>
</asp:Content>
