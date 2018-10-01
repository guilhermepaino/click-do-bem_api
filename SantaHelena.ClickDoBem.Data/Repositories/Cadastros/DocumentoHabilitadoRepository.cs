using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Interfaces.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Data.Repositories.Cadastros
{
    public class DocumentoHabilitadoRepository : RepositorioBase<DocumentoHabilitado>, IDocumentoHabilitadoRepository
    {

        #region Construtores

        /// <summary>
        /// Cria uma nova instância do repositório
        /// </summary>
        /// <param name="ctx">Contexto de banco de dados</param>
        public DocumentoHabilitadoRepository(CdbContext ctx) : base(ctx) { }

        public override DocumentoHabilitado ObterPorId(Guid id)
        {
            string sql = @"SELECT * FROM DocumentoHabilitado WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<DocumentoHabilitado>(sql, new { pid = id }).SingleOrDefault();
        }

        /// <summary>
        /// Obter todos os registros cadastrados
        /// </summary>
        public override IEnumerable<DocumentoHabilitado> ObterTodos()
        {
            string sql = @"SELECT * FROM DocumentoHabilitado";
            return _ctx.Database.GetDbConnection().Query<DocumentoHabilitado>(sql).ToList();
        }

        /// <summary>
        /// Obter registro pelo número do documento (cpf/cnpj)
        /// </summary>
        /// <param name="doc">Número do documento cpf ou cnpj</param>
        public DocumentoHabilitado ObterPorDocumento(string doc)
        {
            string sql = @"SELECT * FROM DocumentoHabilitado WHERE CpfCnpj = @doc";
            return _ctx.Database.GetDbConnection().Query<DocumentoHabilitado>(sql, new { pdoc = doc }).SingleOrDefault();
        }


        #endregion

    }
}
