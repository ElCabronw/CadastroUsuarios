using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.SqlClient;
using Npgsql;

namespace CadastroDeUsuarios.Areas
{
    public class DbSession : IDisposable
    {
       public IDbConnection Connection { get;}
        public DbSession(IConfiguration configuration)
        {
            Connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            Connection.Open();
        }
        public void Dispose() => Connection?.Dispose();
    }
}
