<%@ Page Title="Perfil — Portal GPFA" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilMembro.aspx.cs" Inherits="PortalGpfa.Web.PaginaPerfilMembro" %>
<%@ Import Namespace="PortalGpfa.Web" %>
<%@ Import Namespace="PortalGpfa.Web.Dominio" %>
<asp:Content ID="ConteudoPrincipal" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlMensagem" runat="server" Visible="false" CssClass="estado-vazio">
        <h1><asp:Literal ID="litTituloMensagem" runat="server" Mode="Encode" /></h1>
        <p><asp:Literal ID="litTextoMensagem" runat="server" Mode="Encode" /></p>
        <div class="acoes">
            <asp:HyperLink ID="lnkVoltarMembros" runat="server" CssClass="botao" NavigateUrl="~/Membros.aspx">Voltar ao diretório</asp:HyperLink>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlPerfil" runat="server" Visible="false">
        <header class="perfil-cabecalho">
            <asp:Image ID="imgFoto" runat="server" CssClass="avatar avatar-grande" Width="128" Height="128" />
            <div class="perfil-identidade">
                <p class="rotulo"><asp:Literal ID="litTipo" runat="server" Mode="Encode" /></p>
                <h1><asp:Literal ID="litNome" runat="server" Mode="Encode" /></h1>
                <p class="nota-dados">Perfil fictício criado para demonstração acadêmica.</p>
                <p><asp:Literal ID="litFormacao" runat="server" Mode="Encode" /></p>
                <ul class="links-perfil">
                    <li><asp:HyperLink ID="lnkLattes" runat="server" Target="_blank">Lattes</asp:HyperLink></li>
                    <li><asp:HyperLink ID="lnkOrcid" runat="server" Target="_blank">ORCID</asp:HyperLink></li>
                    <li><asp:HyperLink ID="lnkScholar" runat="server" Target="_blank">Google Scholar</asp:HyperLink></li>
                </ul>
            </div>
        </header>

        <section aria-labelledby="bio-titulo">
            <h2 id="bio-titulo">Biografia</h2>
            <p><asp:Literal ID="litBiografia" runat="server" Mode="Encode" /></p>
        </section>

        <section aria-labelledby="pretensoes-titulo">
            <h2 id="pretensoes-titulo">Pretensões de pesquisa</h2>
            <p><asp:Literal ID="litPretensoes" runat="server" Mode="Encode" /></p>
        </section>

        <section aria-labelledby="artigos-titulo">
            <h2 id="artigos-titulo">Artigos de destaque</h2>
            <p class="nota-dados">Cinco artigos fictícios de demonstração, com links de exemplo.</p>
            <asp:Repeater ID="rptArtigos" runat="server" OnItemDataBound="rptArtigos_ItemDataBound">
                <HeaderTemplate>
                    <ol class="lista-artigos">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <h3><%#: Eval("Titulo") %></h3>
                        <p><%#: Eval("Veiculo") %> — <%#: Eval("Ano") %></p>
                        <asp:HyperLink ID="lnkArtigo" runat="server" CssClass="link-acao">Acessar artigo</asp:HyperLink>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ol>
                </FooterTemplate>
            </asp:Repeater>
        </section>

        <asp:Panel ID="pnlPesquisador" runat="server" Visible="false">
            <section aria-labelledby="orientandos-atuais-titulo">
                <h2 id="orientandos-atuais-titulo">Orientandos atuais</h2>
                <asp:Repeater ID="rptOrientandosAtuais" runat="server">
                    <HeaderTemplate>
                        <ul class="lista-relacoes">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="lnkOrientandoAtual" runat="server" NavigateUrl='<%# "~/PerfilMembro.aspx?id=" + Eval("Id") %>'><%#: Eval("Nome") %></asp:HyperLink>
                            <span class="etiqueta"><%#: Rotulos.TipoMembro((Membro)Container.DataItem) %></span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSemAtuais" runat="server" Visible="false" CssClass="texto-vazio">
                    <p>Nenhum orientando atual registrado.</p>
                </asp:Panel>
            </section>

            <section aria-labelledby="orientandos-passados-titulo">
                <h2 id="orientandos-passados-titulo">Orientandos passados</h2>
                <asp:Repeater ID="rptOrientandosPassados" runat="server">
                    <HeaderTemplate>
                        <ul class="lista-relacoes">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="lnkOrientandoPassado" runat="server" NavigateUrl='<%# "~/PerfilMembro.aspx?id=" + Eval("Id") %>'><%#: Eval("Nome") %></asp:HyperLink>
                            <span class="etiqueta"><%#: Rotulos.TipoMembro((Membro)Container.DataItem) %></span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSemPassados" runat="server" Visible="false" CssClass="texto-vazio">
                    <p>Nenhum orientando passado registrado.</p>
                </asp:Panel>
            </section>

            <section aria-labelledby="projetos-pesquisador-titulo">
                <h2 id="projetos-pesquisador-titulo">Projetos de que participa</h2>
                <asp:Repeater ID="rptProjetosPesquisador" runat="server">
                    <HeaderTemplate>
                        <ul class="lista-relacoes">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="lnkProjetoPesquisador" runat="server" NavigateUrl='<%# "~/ProjetoDetalhe.aspx?id=" + Eval("Id") %>'><%#: Eval("Titulo") %></asp:HyperLink>
                            <span class="etiqueta"><%#: Rotulos.SituacaoProjeto(((Projeto)Container.DataItem).Situacao) %></span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSemProjetosPesquisador" runat="server" Visible="false" CssClass="texto-vazio">
                    <p>Nenhuma participação em projeto registrada.</p>
                </asp:Panel>
            </section>
        </asp:Panel>

        <asp:Panel ID="pnlEstudante" runat="server" Visible="false">
            <section aria-labelledby="orientacao-titulo">
                <h2 id="orientacao-titulo">Orientação</h2>
                <p class="nota-dados">Vínculos marcados como passados são históricos e não representam orientação atual.</p>
                <asp:Repeater ID="rptOrientacoes" runat="server">
                    <HeaderTemplate>
                        <ul class="lista-relacoes">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="lnkOrientador" runat="server" NavigateUrl='<%# "~/PerfilMembro.aspx?id=" + Eval("Pesquisador.Id") %>'><%#: Eval("Pesquisador.Nome") %></asp:HyperLink>
                            <span class="etiqueta"><%#: Rotulos.SituacaoOrientacao(((VinculoOrientacao)Container.DataItem).Atual) %></span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSemOrientacoes" runat="server" Visible="false" CssClass="texto-vazio">
                    <p>Nenhuma orientação registrada.</p>
                </asp:Panel>
            </section>

            <section aria-labelledby="coorientadores-titulo">
                <h2 id="coorientadores-titulo">Coorientadores</h2>
                <asp:Repeater ID="rptCoorientadores" runat="server">
                    <HeaderTemplate>
                        <ul class="lista-relacoes">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <%#: Eval("Nome") %>
                            <span class="etiqueta"><%#: Rotulos.OrigemCoorientador(((Coorientador)Container.DataItem).EhExterno) %></span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSemCoorientadores" runat="server" Visible="false" CssClass="texto-vazio">
                    <p>Nenhum coorientador registrado.</p>
                </asp:Panel>
            </section>

            <section aria-labelledby="projetos-estudante-titulo">
                <h2 id="projetos-estudante-titulo">Projetos vinculados</h2>
                <asp:Repeater ID="rptProjetosEstudante" runat="server">
                    <HeaderTemplate>
                        <ul class="lista-relacoes">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="lnkProjetoEstudante" runat="server" NavigateUrl='<%# "~/ProjetoDetalhe.aspx?id=" + Eval("Id") %>'><%#: Eval("Titulo") %></asp:HyperLink>
                            <span class="etiqueta"><%#: Rotulos.SituacaoProjeto(((Projeto)Container.DataItem).Situacao) %></span>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlSemProjetosEstudante" runat="server" Visible="false" CssClass="texto-vazio">
                    <p>Nenhum projeto vinculado registrado.</p>
                </asp:Panel>
            </section>
        </asp:Panel>

        <asp:Panel ID="pnlTecnico" runat="server" Visible="false">
            <section aria-labelledby="relacoes-tecnico-titulo">
                <h2 id="relacoes-tecnico-titulo">Relações acadêmicas</h2>
                <p>Este perfil técnico não possui orientação nem vínculo de estudante com projetos na demonstração.</p>
            </section>
        </asp:Panel>

        <p><a href='<%= ResolveUrl("~/Membros.aspx") %>' class="link-acao">Voltar ao diretório de membros</a></p>
    </asp:Panel>
</asp:Content>
