using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeUsuarios.Areas;
using CadastroDeUsuarios.Areas.Data;
using CadastroDeUsuarios.Interfaces;
using CadastroDeUsuarios.Models;
using Dapper;
using Npgsql;

namespace CadastroDeUsuarios.Repositories
{
    public class AcessosRepository : Repository<Acessos>, IAcessosRepository
    {
        private readonly UsuariosContext _context;
        private DbSession _db;
        public AcessosRepository(UsuariosContext context,DbSession db) : base(context)
        {
            _context = context;
            _db = db;
        }
        public static IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }
        public async Task<List<Acessos>> GetAcessosAsync()
        {
           
          

            using (var conn = _db.Connection)
            {

                string query = @"SELECT * FROM ""Acessos""";
                //var acessos = (await conn.QueryAsync<Acessos>(sql:query)).ToList();
                try
                {
                    var acessos = await conn.QueryAsync<Acessos>(query);
                    return acessos.ToList();
                }
                catch (Exception ex)
                {
                    return null; 
                }
            



              
            }

        }

        public async Task<Acessos> GetAcessoByName(string nomeAcesso)
        {



            using (var conn = _db.Connection)
            {

                string query = @$"SELECT * FROM ""Acessos"" WHERE ""NivelAcesso"" = '{nomeAcesso}'";
                //var acessos = (await conn.QueryAsync<Acessos>(sql:query)).ToList();
                try
                {
                    var acessos = await conn.QueryAsync<Acessos>(query);
                    return acessos.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }





            }

        }

    }
}
