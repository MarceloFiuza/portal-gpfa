using System;
using System.Web;
using System.Web.SessionState;
using PortalGpfa.Web.Dominio;
using PortalGpfa.Web.Infraestrutura;

namespace PortalGpfa.Web
{
    /// <summary>
    /// Ponto central de acesso ao EstadoPortal da sessão atual.
    /// </summary>
    internal static class SessaoPortal
    {
        private const string ChaveEstado = "PortalGpfa.EstadoPortal";

        public static EstadoPortal Obter()
        {
            HttpContext contexto = HttpContext.Current;
            if (contexto == null)
            {
                throw new InvalidOperationException(
                    "O estado do portal só pode ser obtido durante uma requisição HTTP.");
            }

            HttpSessionState sessao = contexto.Session;
            EstadoPortal estado = sessao[ChaveEstado] as EstadoPortal;

            if (estado == null)
            {
                estado = CargaPortalFicticio.Criar();
                sessao[ChaveEstado] = estado;
            }

            return estado;
        }
    }
}
