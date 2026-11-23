<%@ Page Title="Início — Portal GPFA" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PortalGpfa.Web.PaginaInicial" %>
<asp:Content ID="ConteudoPrincipal" ContentPlaceHolderID="MainContent" runat="server">
    <section class="destaque" aria-labelledby="titulo-portal">
        <p class="rotulo">Programação Web — Parte 1</p>
        <h1 id="titulo-portal">Portal GPFA</h1>
        <p class="chamada">Visibilidade científica e gestão de projetos do Grupo de Pesquisa sobre Formigas Atômicas.</p>
        <p>
            Este portal reúne a apresentação dos membros do grupo, os projetos de pesquisa em andamento e
            concluídos e as relações acadêmicas que os conectam.
        </p>
        <div class="acoes">
            <asp:HyperLink ID="lnkMembros" runat="server" CssClass="botao" NavigateUrl="~/Membros.aspx">Conheça os membros</asp:HyperLink>
            <asp:HyperLink ID="lnkProjetos" runat="server" CssClass="botao botao-secundario" NavigateUrl="~/Projetos.aspx">Veja os projetos</asp:HyperLink>
        </div>
    </section>

    <section aria-labelledby="sobre-titulo">
        <h2 id="sobre-titulo">Sobre o portal</h2>
        <div class="cartoes">
            <article class="cartao">
                <h3>Membros</h3>
                <p>Pesquisadores, estudantes e técnicos do grupo, com formação, biografia e produção científica.</p>
            </article>
            <article class="cartao">
                <h3>Projetos</h3>
                <p>Projetos em andamento e concluídos, com situação, financiamento e equipe participante.</p>
            </article>
            <article class="cartao">
                <h3>Relações acadêmicas</h3>
                <p>Orientações atuais e passadas, coorientações e vínculos entre estudantes e projetos.</p>
            </article>
        </div>
    </section>

    <section class="aviso" aria-labelledby="etapa-titulo">
        <h2 id="etapa-titulo">Demonstração em memória</h2>
        <p>
            O diretório de membros, os perfis e a área de projetos já utilizam dados fictícios mantidos na
            sessão do servidor, isolados por navegador. O estado é recriado quando a sessão expira ou o servidor
            reinicia. Nenhum perfil, artigo, projeto ou valor deve ser interpretado como informação real.
        </p>
    </section>
</asp:Content>
