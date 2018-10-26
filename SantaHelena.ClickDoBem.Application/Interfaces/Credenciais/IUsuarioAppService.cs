using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using System;
using System.Collections.Generic;

namespace SantaHelena.ClickDoBem.Application.Interfaces.Credenciais
{

    public interface IUsuarioAppService : IAppServiceBase
    {
        bool Autenticar(string usuario, string senha, out string mensagem, out UsuarioDto usuarioDto);
        UsuarioDto ObterPorId(Guid id);
        IEnumerable<UsuarioDto> ObterTodos();
        IEnumerable<UsuarioDto> ObterPorPerfil(string perfil);
        void CadastrarColaborador(UsuarioDto dto, out int statusCode, out object dados);
        ArquivoDocumentoDto ImportarArquivoColaborador(IFormFile arquivo, string caminho, out int statusCode);
        void VerificarSituacaoDocumento(string documento, out string situacao, out bool cadastrado);
        bool EsqueciSenha(string cpfCnpj, DateTime? dataNascimento, string novaSenha, string confirmarSenha, out int statusCode, out string mensagem);
    }

}