using Devvo.CrossCutting.Util.Enum;

namespace Devvo.Application.ViewModel
{
    public class AnelViewModel
    {
        public AnelViewModel() { }
        public AnelViewModel(Guid id, string nome, string poder, string portador, string forjadoPor, string imagem, EAnel tipo, string usuarioId)
        {
            Id = id;
            Nome = nome;
            Poder = poder;
            Portador = portador;
            ForjadoPor = forjadoPor;
            Imagem = imagem;
            Tipo = tipo;
            UsuarioId = usuarioId;
        }

        public AnelViewModel(string nome, string poder, string portador, string forjadoPor, EAnel tipo, string usuarioId)
        {
            Nome = nome;
            Poder = poder;
            Portador = portador;
            ForjadoPor = forjadoPor;
            Tipo = tipo;
            UsuarioId = usuarioId;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Poder { get; set; } = string.Empty;
        public string Portador { get; set; } = string.Empty;
        public string ForjadoPor { get; set; } = string.Empty;
        public string Imagem { get; set; } = string.Empty;
        public EAnel Tipo { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
    }
}
