using System;

namespace PortalGpfa.Web
{
    internal static class Urls
    {
        public static bool EhHttp(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            Uri endereco;
            return Uri.TryCreate(valor, UriKind.Absolute, out endereco) &&
                (endereco.Scheme == Uri.UriSchemeHttp || endereco.Scheme == Uri.UriSchemeHttps);
        }
    }
}
