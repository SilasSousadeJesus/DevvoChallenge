using Devvo.Application.DTOs.Anel;
using Devvo.Application.Interfaces;
using Devvo.Application.ViewModel;
using Devvo.CrossCutting.Extensions;
using Devvo.CrossCutting.Util.Dicionario;
using Devvo.CrossCutting.Util.Enum;
using Devvo.Domain.Entities;
using Devvo.Infra.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Devvo.Application.Services
{
    public class AnelAppService : IAnelAppService
    {

        private readonly IAnelRepository _anelRepository;
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IConfiguration _configuration;

        public AnelAppService(IAnelRepository anelRepository, IUsuarioAppService usuarioAppService, IConfiguration configuration)
        {
            _anelRepository = anelRepository;
            _usuarioAppService = usuarioAppService;
            _configuration = configuration;
        }

        public async Task<RetornoGenerico> BuscarTodosOsElementosAsync(string id)
        {
            try
            {
                var usuarioValido = await ValidarUsuario(id);
                if (!usuarioValido.Sucesso)
                    return usuarioValido;

                var lista = await _anelRepository.BuscarTodosOsElementosAsync(id);

                var listaViewModel = lista.Select(x => new AnelViewModel(
                                                        x.Id,
                                                        x.Nome,
                                                        x.Poder,
                                                        x.Portador,
                                                        x.ForjadoPor,
                                                        x.Imagem,
                                                        x.Tipo,
                                                        x.UsuarioId
                                                    )).ToList() ?? new List<AnelViewModel>();

                return CriarRetorno(HttpStatusCode.OK, true, $"{listaViewModel.Count} elemento(s) encontrado(s)", listaViewModel);
            }
            catch (Exception ex)
            {
                return CriarRetorno(HttpStatusCode.InternalServerError, false, ex.ToString(), null, "Não foi possível buscar a lista de aneis");
            }
        }

        public async Task<RetornoGenerico> BuscarUmElementoAsync(string usuarioId, Guid elementoId)
        {
            var retorno = new RetornoGenerico();

            try
            {
                var usuarioValido = await ValidarUsuario(usuarioId);
                if (!usuarioValido.Sucesso)
                    return usuarioValido;

                var anel = await _anelRepository.BuscarUmElementoAsync(usuarioId, elementoId);


                return CriarRetorno(
                        anel != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, 
                        anel != null ? true : false, 
                        anel != null ? $"Anel encontrado" : "Não foi possivel encontrar o anel",
                        anel
                        );
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.HttpStatusCode = HttpStatusCode.InternalServerError;
                retorno.MensagemSistema = $"{ex}";
                retorno.MensagemUsuario = "Não foi possivel encontrar o anel";
                retorno.Dados = null;
                return retorno;
            }
        }

        public async  Task<RetornoGenerico> CadastrarElementoAsync(CadastrarAnelDTO elementoDTO)
        {
            try
            {
                var usuarioValido = await ValidarUsuario(elementoDTO.UsuarioId);
                if (!usuarioValido.Sucesso)
                    return usuarioValido;

                var valido = await ValidarCadastroAnel(elementoDTO.UsuarioId, elementoDTO.Tipo);

               
                if (!valido) {

                    var donoDoAnel =  MaxTipoAnelDicionario.ObterMensagem(elementoDTO.Tipo);

                    return CriarRetorno(
                       HttpStatusCode.BadRequest, 
                       false,
                        $"O grande mago JRR Tolkien nos deixou uma frase famosa: \r\n\r\n Três Anéis para os Reis Élficos sob o céu,\r\nSete para os Senhores Anões em seus salões de pedra,\r\nNove para Homens Mortais condenados a morrer,\r\nUm para o Senhor das Trevas em seu trono escuro\r\nNa Terra de Mordor onde as Sombras jazem.\r\nUm Anel para governar a todos, Um Anel para encontrá-los,\r\nUm Anel para trazê-los a todos, e na escuridão prendê-los\r\nNa Terra de Mordor onde as Sombras jazem.",
                       donoDoAnel
                     );

                }

                var anel = new Anel(
                    elementoDTO.Nome,
                    elementoDTO.Poder,
                    elementoDTO.Portador,
                    elementoDTO.ForjadoPor,
                    elementoDTO.Tipo,
                    elementoDTO.UsuarioId
                    );

                anel.AtribuirImagem(anel.Tipo);

                var resultado = await _anelRepository.CadastrarElementoAsync(anel);

                return CriarRetorno(
                        resultado ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, resultado, 
                        resultado ? $"Anel criado com sucesso": "Não foi possivel criar o anel", 
                        null
                    );
            }
            catch (Exception ex)
            {
                return CriarRetorno(HttpStatusCode.InternalServerError, false, ex.ToString(), null, "Não foi possível criar o anel");
            }
        }

        public async Task<RetornoGenerico> DeletarElementoAsync(string idPatrono, Guid idElemento)
        {
            try
            {
                var usuarioValido = await ValidarUsuario(idPatrono);
                if (!usuarioValido.Sucesso)
                    return usuarioValido;

                var anelResultado = await BuscarUmElementoAsync(idPatrono, idElemento);

                if (anelResultado.Dados is null) {
                    return CriarRetorno(
                                HttpStatusCode.NotFound, 
                                false,
                               "Anel não encontrado",
                                null
                            );
                }

                var resultado = await _anelRepository.DeletarElementoAsync((Anel)anelResultado.Dados);

                return CriarRetorno(
                        resultado ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, resultado,
                        resultado ? $"Anel deletado com sucesso" : "Não foi possivel criar o anel",
                        null
                    );
            }
            catch (Exception ex)
            {
                return CriarRetorno(HttpStatusCode.InternalServerError, false, ex.ToString(), null, "Não foi possível deletar o anel");
            }
        }

        public async Task<RetornoGenerico> EditarElementoAsync(string idPatrono, Guid elementoId, EditarAnelDTO elementoDTO)
        {
            try
            {
                var usuarioValido = await ValidarUsuario(idPatrono);
                if (!usuarioValido.Sucesso)
                    return usuarioValido;

                var anelResultado = await BuscarUmElementoAsync(idPatrono, elementoId);

                if (anelResultado.Dados is null)
                {
                    return CriarRetorno(
                                HttpStatusCode.NotFound,
                                false,
                               "Anel não encontrado",
                                null
                            );
                }

                Anel anelCorrente = anelResultado.Dados;
                Anel anelAtualizado = new Anel(
                    elementoDTO.Nome,
                    elementoDTO.Poder,
                    elementoDTO.Portador,
                    elementoDTO.ForjadoPor,
                    elementoDTO.Tipo,
                    idPatrono
                    );


                if ((int)anelCorrente.Tipo != (int)anelAtualizado.Tipo)
                {
                    var valido = await ValidarCadastroAnel(usuarioValido.Dados.Id, anelAtualizado.Tipo);

                    if (!valido)
                    {
                        var donoDoAnel = MaxTipoAnelDicionario.ObterMensagem(elementoDTO.Tipo);

                        return CriarRetorno(
                           HttpStatusCode.BadRequest,
                           false,
                            $"O grande mago JRR Tolkien nos deixou uma frase famosa: \r\n\r\n Três Anéis para os Reis Élficos sob o céu,\r\nSete para os Senhores Anões em seus salões de pedra,\r\nNove para Homens Mortais condenados a morrer,\r\nUm para o Senhor das Trevas em seu trono escuro\r\nNa Terra de Mordor onde as Sombras jazem.\r\nUm Anel para governar a todos, Um Anel para encontrá-los,\r\nUm Anel para trazê-los a todos, e na escuridão prendê-los\r\nNa Terra de Mordor onde as Sombras jazem.",
                           donoDoAnel
                         );

                    }
                }

                        
                anelCorrente.EditarAnel(anelAtualizado);

                anelCorrente.AtribuirImagem(anelCorrente.Tipo);

                var resultado = await _anelRepository.EditarElementoAsync(anelCorrente);

                return CriarRetorno(
                        resultado ? HttpStatusCode.OK : HttpStatusCode.InternalServerError, resultado,
                        resultado ? $"Anel editado com sucesso" : "Não foi possivel editar o anel",
                        null
                    );
            }
            catch (Exception ex)
            {
                return CriarRetorno(HttpStatusCode.InternalServerError, false, ex.ToString(), null, "Não foi possível editar o anel");
            };
        }



        private async Task<bool> ValidarCadastroAnel(string id, EAnel tipoAnel)
        {
            var maxTipoAnelKey = $"MaxTipoAnel:{Enum.GetValues(typeof(EAnel))
                             .Cast<EAnel>()
                             .FirstOrDefault(x => (int)x == (int)tipoAnel)
                             .ToString()}";

            var maxTipoAnelValue = _configuration.GetSection(maxTipoAnelKey).Value;

            if (!int.TryParse(maxTipoAnelValue, out var maxTipoAnel))
            {
                return false;
            }

            var quantidade = await _anelRepository.BuscarTodosPorTipoAsync(id, tipoAnel);

            return quantidade >= maxTipoAnel ? false : true;
        }

        private async Task<RetornoGenerico> ValidarUsuario(string id)
        {
            var buscaPorUsuario = await _usuarioAppService.BuscarUmUsuario(id);

            if (!buscaPorUsuario.Sucesso)
            {
                return CriarRetorno(
                    HttpStatusCode.NotFound,
                    false,
                    buscaPorUsuario.MensagemSistema,
                    buscaPorUsuario.Dados,
                    buscaPorUsuario.MensagemUsuario
                );
            }

            return CriarRetorno(
                   HttpStatusCode.OK,
                   true ,
                   buscaPorUsuario.MensagemSistema,
                   buscaPorUsuario.Dados,
                   buscaPorUsuario.MensagemUsuario
               ); 
        }

        private RetornoGenerico CriarRetorno(HttpStatusCode statusCode, bool sucesso, string mensagemSistema, object dados = null, string mensagemUsuario = null)
        {
            return new RetornoGenerico
            {
                HttpStatusCode = statusCode,
                Sucesso = sucesso,
                MensagemSistema = mensagemSistema,
                MensagemUsuario = mensagemUsuario ?? mensagemSistema,
                Dados = dados
            };
        }
    }
}
