using Dapper;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Context;
using SantaHelena.ClickDoBem.Domain.Entities;
using SantaHelena.ClickDoBem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SantaHelena.ClickDoBem.Data.Repositories
{
    public class UsuarioRepository : RepositorioBase<Usuario>, IUsuarioRepository
    {

        #region Construtores

        public UsuarioRepository(CdbContext ctx) : base(ctx) { }


        #endregion

        #region Métodos Públicos

        public override Usuario FindById(Guid id)
        {

            string sql = @"SELECT * FROM Usuario WHERE Id = @pid";
            return _ctx.Database.GetDbConnection().Query<Usuario>
            (
                sql,
                new { pid = id }
            ).SingleOrDefault();

        }

        public override IEnumerable<Usuario> GetAll()
        {
            string sql = @"SELECT * FROM Usuario";
            return _ctx.Database.GetDbConnection().Query<Usuario>(sql).ToList();
        }

        #endregion

    }
}
