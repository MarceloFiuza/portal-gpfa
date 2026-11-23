<%@ Page Title="Membros — Portal GPFA" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Membros.aspx.cs" Inherits="PortalGpfa.Web.PaginaMembros" %>
<%@ Import Namespace="PortalGpfa.Web" %>
<%@ Import Namespace="PortalGpfa.Web.Dominio" %>
<asp:Content ID="ConteudoPrincipal" ContentPlaceHolderID="MainContent" runat="server">
    <header class="cabecalho-pagina">
        <h1>Membros</h1>
        <p>Diretório público de pesquisadores, estudantes e técnicos do GPFA.</p>
        <p class="nota-dados">Todos os perfis desta demonstração são fictícios.</p>
    </header>

    <div class="grade-membros">
        <asp:Repeater ID="rptMembros" runat="server">
            <ItemTemplate>
                <article class="cartao-membro">
                    <img class="avatar" src='<%#: ResolveUrl((string)Eval("FotoUrl")) %>' alt='<%#: "Avatar fictício de " + Eval("Nome") %>' width="96" height="96" />
                    <div class="cartao-membro-conteudo">
                        <h2>
                            <asp:HyperLink ID="lnkPerfil" runat="server" NavigateUrl='<%# "~/PerfilMembro.aspx?id=" + Eval("Id") %>'><%#: Eval("Nome") %></asp:HyperLink>
                        </h2>
                        <p class="tipo-membro"><%#: Rotulos.TipoMembro((Membro)Container.DataItem) %></p>
                    </div>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:Panel ID="pnlVazio" runat="server" Visible="false" CssClass="estado-vazio">
        <h2>Nenhum membro cadastrado</h2>
        <p>O diretório está vazio no estado atual da sessão.</p>
    </asp:Panel>
</asp:Content>
