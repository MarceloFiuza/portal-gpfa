using System;

namespace PortalGpfa.Web.Dominio
{
    public enum SituacaoProjeto
    {
        EmAndamento,
        Concluido
    }

    public sealed class Projeto
    {
        public int Id { get; }
        public string Titulo { get; }
        public string Descricao { get; }
        public SituacaoProjeto Situacao { get; }
        public decimal? VerbaCaptada { get; }
        public Pesquisador Coordenador { get; }

        public Projeto(
            int id,
            string titulo,
            string descricao,
            SituacaoProjeto situacao,
            decimal? verbaCaptada,
            Pesquisador coordenador)
        {
            Id = Guarda.Positivo(id, nameof(id));
            Titulo = Guarda.Texto(titulo, nameof(titulo));
            Descricao = Guarda.Texto(descricao, nameof(descricao));

            if (situacao != SituacaoProjeto.EmAndamento && situacao != SituacaoProjeto.Concluido)
            {
                throw new ArgumentOutOfRangeException(nameof(situacao), "A situação do projeto é inválida.");
            }

            if (verbaCaptada.HasValue && verbaCaptada.Value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(verbaCaptada), "A verba captada não pode ser negativa.");
            }

            Situacao = situacao;
            VerbaCaptada = verbaCaptada;
            Coordenador = Guarda.NaoNulo(coordenador, nameof(coordenador));
        }
    }
}
