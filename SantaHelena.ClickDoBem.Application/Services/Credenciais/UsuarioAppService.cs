using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Application.Dto.Cadastros;
using System.Net.Http.Headers;
using System.IO;
using SantaHelena.ClickDoBem.Domain.Core.Tools;
using SantaHelena.ClickDoBem.Domain.Core.Enums;

namespace SantaHelena.ClickDoBem.Application.Services.Credenciais
{

    /// <summary>
    /// Objeto de serviço de Usuario
    /// </summary>
    public class UsuarioAppService : AppServiceBase<UsuarioDto, Usuario>, IUsuarioAppService
    {

        #region Objetos/Variáveis Locais

        protected readonly IUnitOfWork _uow;
        protected readonly IUsuarioDomainService _dmn;
        protected readonly IDocumentoHabilitadoDomainService _docHabDomain;

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public UsuarioAppService
        (
            IUnitOfWork uow,
            IUsuarioDomainService dmn,
            IDocumentoHabilitadoDomainService docHabDomain
        )
        {
            _uow = uow;
            _dmn = dmn;
            _docHabDomain = docHabDomain;
        }

        #endregion

        #region Overrides

        protected Usuario ConverterDtoEmEntidade(UsuarioDto dto)
        {

            if (dto == null)
                return null;

            Usuario usuario = new Usuario()
            {
                CpfCnpj = dto.CpfCnpj,
                Nome = dto.Nome
            };

            usuario.UsuarioLogin = new UsuarioLogin()
            {
                UsuarioId = usuario.Id,
                Login = dto.UsuarioLogin.Login,
                Senha = dto.UsuarioLogin.Senha
            };

            usuario.UsuarioDados = new UsuarioDados()
            {
                UsuarioId = usuario.Id,
                DataNascimento = dto.UsuarioDados.DataNascimento,
                Logradouro = dto.UsuarioDados.Logradouro,
                Numero = dto.UsuarioDados.Numero,
                Complemento = dto.UsuarioDados.Complemento,
                Bairro = dto.UsuarioDados.Bairro,
                Cidade = dto.UsuarioDados.Cidade,
                UF = dto.UsuarioDados.UF,
                CEP = dto.UsuarioDados.CEP,
                TelefoneFixo = dto.UsuarioDados.TelefoneFixo,
                TelefoneCelular = dto.UsuarioDados.TelefoneCelular,
                Email = dto.UsuarioDados.Email
            };

            ConverterDtoPerfilEmEntidadePerfil(usuario, dto.UsuarioPerfil);

            return usuario;

        }

        protected void ConverterDtoPerfilEmEntidadePerfil(Usuario usuario, IEnumerable<string> perfisDto)
        {

            if (perfisDto.Count() > 0)
            {
                perfisDto
                    .ToList()
                    .ForEach(p =>
                    {
                        usuario.Perfis.Add(new UsuarioPerfil() { UsuarioId = usuario.Id, Perfil = p });
                    });

            }
        }

        protected override UsuarioDto ConverterEntidadeEmDto(Usuario usuario)
        {

            if (usuario == null)
                return null;

            UsuarioDto uDto = new UsuarioDto()
            {
                Id = usuario.Id,
                DataInclusao = usuario.DataInclusao,
                DataAlteracao = usuario.DataAlteracao,
                CpfCnpj = usuario.CpfCnpj,
                Nome = usuario.Nome
            };

            if (usuario.UsuarioLogin != null)
                uDto.UsuarioLogin = new UsuarioLoginDto()
                {
                    Login = usuario.UsuarioLogin.Login,
                    Senha = "*ENCRYPTED*"
                };

            if (usuario.UsuarioDados != null)
                uDto.UsuarioDados = new UsuarioDadosDto()
                {
                    Id = usuario.UsuarioDados.Id,
                    DataInclusao = usuario.UsuarioDados.DataInclusao,
                    DataAlteracao = usuario.UsuarioDados.DataAlteracao,
                    DataNascimento = usuario.UsuarioDados.DataNascimento,
                    Logradouro = usuario.UsuarioDados.Logradouro,
                    Numero = usuario.UsuarioDados.Numero,
                    Complemento = usuario.UsuarioDados.Complemento,
                    Bairro = usuario.UsuarioDados.Bairro,
                    Cidade = usuario.UsuarioDados.Cidade,
                    UF = usuario.UsuarioDados.UF,
                    CEP = usuario.UsuarioDados.CEP,
                    TelefoneCelular = usuario.UsuarioDados.TelefoneCelular,
                    TelefoneFixo = usuario.UsuarioDados.TelefoneFixo,
                    Email = usuario.UsuarioDados.Email
                };

            if (usuario.Perfis != null)
                uDto.UsuarioPerfil = usuario.Perfis.Select(x => x.Perfil).ToList();

            return uDto;

        }

        #endregion

        #region Métodos Públicos

        public UsuarioDto ObterPorId(Guid id)
        {

            Usuario usuario = _dmn.ObterPorId(id);
            if (usuario == null)
                return null;

            return ConverterEntidadeEmDto(usuario);

        }

        /// <summary>
        /// Obter todos os registros
        /// </summary>
        public IEnumerable<UsuarioDto> ObterTodos()
        {

            IEnumerable<Usuario> result = _dmn.ObterTodos();
            if (result == null)
                return null;

            return
                (
                    from r in result
                    select ConverterEntidadeEmDto(r)

                ).ToList();

        }

        /// <summary>
        /// Autenticar usuário através de usuário e senha
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="senha">Senha do usuário (Hash Md5)</param>
        /// <param name="mensagem">Mensagem de saída do resultado da autenticação</param>
        /// <param name="usuarioDto">Objeto Dto para saida do usuário</param>
        public bool Autenticar(string usuario, string senha, out string mensagem, out UsuarioDto usuarioDto)
        {

            Usuario usr = _dmn.ObterPorLogin(usuario, senha);
            usuarioDto = ConverterEntidadeEmDto(usr);
            if (usr == null)
            {
                mensagem = "Usuário e/ou senha inválido!";
                return false;
            }
            mensagem = "Usuário autenticado";
            return true;
        }

        /// <summary>
        /// Obter registros pelo perfil
        /// </summary>
        /// <param name="perfil">Perfil de filtro</param>
        public IEnumerable<UsuarioDto> ObterPorPerfil(string perfil)
        {
            IList<UsuarioDto> result = new List<UsuarioDto>();
            _dmn.ObterPorPerfil(perfil)
                .ToList()
                .ForEach(ud =>
                {
                    result.Add(ConverterEntidadeEmDto(ud));
                });
            return result;
        }

        /// <summary>
        /// Cadastrar um novo colaborador
        /// </summary>
        /// <param name="dto">Objeto Data-Transport</param>
        /// <param name="statusCode">Variável de saído do código de status</param>
        /// <param name="dados">Variável de saída da mensagem</param>
        public void CadastrarColaborador(UsuarioDto dto, out int statusCode, out object dados)
        {

            // Verificar DocumentoHabilitado
            DocumentoHabilitado documento = _docHabDomain.ObterPorDocumento(dto.CpfCnpj);
            if (documento == null || !documento.Ativo)
            {
                statusCode = StatusCodes.Status404NotFound;
                dados = new { sucesso = false, mensagem = "Entre em contato com RH" };
            }
            else
            {

                // Verificar se o usuário já está cadastrado
                Usuario usuario = _dmn.ObterPorDocumento(dto.CpfCnpj);
                if (usuario != null)
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    dados = new { sucesso = false, mensagem = "Usuário já cadastrado" };
                }
                else
                {

                    Usuario entidade = ConverterDtoEmEntidade(dto);

                    if (!entidade.EstaValido())
                    {
                        dados = new { sucesso = false, mensagem = entidade.ValidationResult.ToString() };
                        statusCode = StatusCodes.Status400BadRequest;
                    }
                    else
                    {

                        _dmn.Adicionar(entidade);
                        _uow.Efetivar();

                        dados = new { sucesso = true, mensagem = new { Id = entidade.Id } };
                        statusCode = StatusCodes.Status200OK;

                    }

                }

            }

        }

        /// <summary>
        /// Processar arquivo de colaboradores
        /// </summary>
        /// <param name="arquivo"></param>
        /// <param name="caminho"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public ArquivoDocumentoDto ImportarArquivoColaborador(IFormFile arquivo, string caminho, out int statusCode)
        {

            // Preparando nome e caminho de arquivo
            ArquivoDocumentoDto adt = new ArquivoDocumentoDto() { NomeArquivo = arquivo.FileName };
            string nomeArquivoBase = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');
            string nomeCompleto = Path.Combine(caminho, nomeArquivoBase);
            statusCode = StatusCodes.Status200OK;

            // Validando arquivo
            //if (!arquivo.ContentType.ToLower().Contains("application/vnd.ms-excel") || !arquivo.FileName.ToLower().EndsWith(".csv"))
            if (!arquivo.FileName.ToLower().EndsWith(".csv"))
            {
                statusCode = StatusCodes.Status400BadRequest;
                adt.Sucesso = false;
                adt.Detalhe = "Arquivo inválido";
            }
            else
            {

                // Ler arquivo
                bool documentoValido = true;
                try
                {

                    // Salvar arquivo
                    using (FileStream fs = new FileStream(nomeCompleto, FileMode.Create))
                    {
                        arquivo.CopyTo(fs);
                    }

                    int numLinha = 0;
                    using (StreamReader sr = new StreamReader(nomeCompleto))
                    {

                        IList<string> docsProcessados = new List<string>();
                        string linha;
                        while ((linha = sr.ReadLine()) != null)
                        {

                            numLinha++;
                            if (numLinha > 1)
                            {

                                LinhaArquivoDocumentoDto dadosLinha = new LinhaArquivoDocumentoDto
                                {
                                    Linha = numLinha,
                                    Conteudo = linha
                                };

                                string[] campos = linha.Split(',');
                                if (campos.Length.Equals(2))
                                {

                                    string documento = Misc.LimparNumero(campos[0]);
                                    string situacao = campos[1].ToUpper();

                                    if (docsProcessados.Any(x => x.Equals(documento)))
                                    {

                                        dadosLinha.Sucesso = false;
                                        documentoValido = false;
                                        dadosLinha.Acao = AcaoDocumento.DocumentoDuplicado;
                                        dadosLinha.Detalhe = EnumHelper.GetEnumDescription(AcaoDocumento.DocumentoDuplicado);

                                    }
                                    else
                                    {

                                        if (!Check.VerificarDocumento(documento))
                                            dadosLinha.Detalhe = "Documento inválido";

                                        if (!(situacao.Equals("S") || situacao.Equals("N")))
                                            dadosLinha.Detalhe = $"{(dadosLinha.Detalhe.Length.Equals(0) ? string.Empty : $"{dadosLinha.Detalhe} / ")}Situação inválida";

                                        if (!string.IsNullOrEmpty(dadosLinha.Detalhe))
                                        {
                                            dadosLinha.Acao = AcaoDocumento.DocumentoInvalido;
                                            dadosLinha.Detalhe = EnumHelper.GetEnumDescription(AcaoDocumento.DocumentoInvalido);
                                            dadosLinha.Sucesso = false;
                                            documentoValido = false;
                                        }
                                        else
                                        {
                                            // Persistir na base
                                            dadosLinha.Sucesso = true;
                                            dadosLinha.Acao = AcaoDocumento.Alteracao;
                                            dadosLinha.Detalhe = EnumHelper.GetEnumDescription(AcaoDocumento.Alteracao);
                                            DocumentoHabilitado doc = _docHabDomain.ObterPorDocumento(documento);
                                            if (doc == null)
                                            {
                                                dadosLinha.Acao = AcaoDocumento.Inclusao;
                                                dadosLinha.Detalhe = EnumHelper.GetEnumDescription(AcaoDocumento.Inclusao);
                                                doc = new DocumentoHabilitado()
                                                {
                                                    CpfCnpj = documento
                                                };

                                            }
                                            doc.Ativo = situacao.Equals("S");
                                            if (dadosLinha.Acao == AcaoDocumento.Inclusao)
                                                _docHabDomain.Adicionar(doc);
                                            else
                                                _docHabDomain.Atualizar(doc);

                                            docsProcessados.Add(documento);

                                        }

                                    }

                                }
                                else
                                {
                                    dadosLinha.Sucesso = false;
                                    documentoValido = false;
                                    dadosLinha.Detalhe = "Layout da linha inválido";
                                }


                                adt.Linhas.Add(dadosLinha);

                            }

                        }
                    }

                    // Commit
                    if (documentoValido)
                    {
                        _uow.Efetivar();
                        adt.Sucesso = true;
                        adt.Detalhe = "Arquivo processado com sucesso";
                    }
                    else
                    {
                        adt.Sucesso = false;
                        statusCode = StatusCodes.Status400BadRequest;
                        adt.Detalhe = "Arquivo processado e criticado, veja detalhes das linhas;";
                    }

                }
                catch (Exception ex)
                {
                    adt.Sucesso = false;
                    adt.Detalhe = $"Falha no processamento do arquivo: {ex.Message}-{ex.StackTrace}";
                }

                try { File.Delete(nomeCompleto); }
                finally { /* Nada a fazer, segue o jogo */ }

            }

            return adt;

        }

        /// <summary>
        /// Retorna o status de um documento
        /// </summary>
        /// <param name="documento">Documento a ser verificado</param>
        /// <param name="situacao">Variável de saída e situação</param>
        /// <param name="cadastrado">Variável de saída de flag de situação de cadastro completo</param>
        public void VerificarSituacaoDocumento(string documento, out string situacao, out bool cadastrado)
        {
            _dmn.VerificarSituacaoDocumento(documento, out situacao, out cadastrado);
        }

        #endregion

    }
}
