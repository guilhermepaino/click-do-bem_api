using SantaHelena.ClickDoBem.Application.Interfaces.Credenciais;
using SantaHelena.ClickDoBem.Application.Dto.Credenciais;
using SantaHelena.ClickDoBem.Domain.Core.Interfaces;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using SantaHelena.ClickDoBem.Domain.Interfaces.Credenciais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #endregion

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do ApplicationService
        /// </summary>
        public UsuarioAppService
        (
            IUnitOfWork uow,
            IUsuarioDomainService dmn
        )
        {
            _uow = uow;
            _dmn = dmn;
        }

        #endregion

        #region Overrides

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
            {
                uDto.UsuarioPerfil =
                (
                    from p in usuario.Perfis
                    select new UsuarioPerfilDto()
                    {
                        Perfil = p.Perfil
                    }
                ).ToList();
            }

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

        #endregion

    }
}
