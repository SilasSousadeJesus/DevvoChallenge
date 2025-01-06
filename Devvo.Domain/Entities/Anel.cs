using Devvo.CrossCutting.Util.Enum;
using System.ComponentModel.DataAnnotations;

namespace Devvo.Domain.Entities
{
    public class Anel
    {
        public Anel() { }
        public Anel(
            string nome, 
            string poder, 
            string portador, 
            string forjadoPor, 
            EAnel tipo, 
            string usuarioId) { 

            Nome = nome;
            Poder = poder;
            Portador = portador;
            ForjadoPor = forjadoPor;
            Tipo = tipo;
            UsuarioId = usuarioId;
        }

        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Poder { get; set; } = string.Empty;
        public string Portador { get; set; } = string.Empty;
        public string ForjadoPor { get; set; } = string.Empty;
        public string Imagem { get; set; } = string.Empty;
        public EAnel Tipo { get; set; } = EAnel.NaoIdentificado;
        public string UsuarioId { get; set; } = string.Empty;

        public void EditarAnel(Anel anel)
        {
            if (anel == null) return;

            if (!string.IsNullOrWhiteSpace(anel.Nome) && !anel.Nome.Equals(Nome, StringComparison.Ordinal))
            {
                Nome = anel.Nome;
            }
            if (!string.IsNullOrWhiteSpace(anel.Poder) && !anel.Poder.Equals(Poder, StringComparison.Ordinal))
            {
                Poder = anel.Poder;
            }
            if (!string.IsNullOrWhiteSpace(anel.Portador) && !anel.Portador.Equals(Portador, StringComparison.Ordinal))
            {
                Portador = anel.Portador;
            }

            if (!string.IsNullOrWhiteSpace(anel.ForjadoPor) && !anel.ForjadoPor.Equals(ForjadoPor, StringComparison.Ordinal))
            {
                ForjadoPor = anel.ForjadoPor;
            }

            //if (!string.IsNullOrWhiteSpace(anel.Imagem) && !anel.Imagem.Equals(Imagem, StringComparison.Ordinal))
            //{
            //    Imagem = anel.Imagem;
            //}

            if (anel.Tipo != Tipo && anel.Tipo != EAnel.NaoIdentificado)
            {
                Tipo = anel.Tipo;
            }
        }

        public void AtribuirImagem(EAnel tipo) {
            if (tipo == EAnel.ParaHomens) Imagem = "/assets/images/ParaHomens.jpg";
            if (tipo == EAnel.ParaElfos) Imagem = "/assets/images/ParaElfos.jpg";
            if (tipo == EAnel.ParaAnoes) Imagem = "/assets/images/ParaAnoes.jpg";
            if (tipo == EAnel.ParaSauron) Imagem = "/assets/images/ParaSauron.jpg";
            if (tipo == EAnel.NaoIdentificado) Imagem = "/assets/images/Generica.jpg";
        }
  
    
    }
}
