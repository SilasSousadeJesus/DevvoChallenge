using Devvo.CrossCutting.Util.Enum;

namespace Devvo.CrossCutting.Util.Dicionario
{
    public static class MaxTipoAnelDicionario
    {
        private static readonly Dictionary<EAnel, string> _mensagens = new()
    {
        { EAnel.ParaElfos, "SÓ SÃO PERMITIDOS ATÉ 3 ANÉIS PARA ELFOS" },
        { EAnel.ParaAnoes, "SÓ SÃO PERMITIDOS ATÉ 7 ANÉIS PARA ANÕES" },
        { EAnel.ParaHomens, "SÓ SÃO PERMITIDOS ATÉ 9 ANÉIS PARA HOMENS" },
        { EAnel.ParaSauron, "SÓ É PERMITIDO 1 ANEL PARA SAURON" }
    };

        public static string ObterMensagem(EAnel tipoAnel)
        {
            return _mensagens.ContainsKey(tipoAnel)
                ? _mensagens[tipoAnel]
                : "Tipo de anel desconhecido";
        }
    }

}
