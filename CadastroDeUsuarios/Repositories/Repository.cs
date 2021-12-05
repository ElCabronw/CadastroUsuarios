using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CadastroDeUsuarios.Areas.Data;
using CadastroDeUsuarios.Interfaces;
using CadastroDeUsuarios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CadastroDeUsuarios.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>  where TEntity : class
    {
        protected readonly UsuariosContext Db;
        protected readonly IConfiguration _configuration;
        protected readonly string stringConnection;

        

        protected Repository(UsuariosContext context)
        {
            //Db = context;
            this.Db = context;
           // this.stringConnection = IConfiguration.GetConnectionString("DefaultConnection");
        }



        public virtual void Adicionar(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.Entry(obj).State = EntityState.Added;
            Save();
        }

        public virtual IEnumerable<TEntity> ObterTodos()
        {
            return Db.Set<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            
            Expression<Func<TEntity, bool>> query = predicate;
            return Db.Set<TEntity>().Where(query);
        }
        public virtual void Remover(long id)
        {
            //DbSet.Remove(DbSet.Find(id));
            var obj = Db.Set<TEntity>().Find(id);
            Db.Entry(obj).State = EntityState.Deleted;
            Save();
        }

        public virtual void AtualizarNomeUsuario(ApplicationUser obj)
        {
            try
            {
                var user = Db.Set<ApplicationUser>().Find(obj.Id);

                user.Nome = obj.Nome; 
                Save();
            }
            catch (Exception e)
            {

            }
        }
       

        private void Save()
        {
            //Db.SaveChanges();

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var sb = new StringBuilder();
                //sb.AppendLine($"DbUpdateException error details - {e?.InnerException?.InnerException?.Message}");
                sb.AppendLine($"Erro: detalhe técnico::: {e?.InnerException?.InnerException?.Message}");

                foreach (var eve in e.Entries)
                {
                    sb.AppendLine($"Objeto [{eve.Entity.GetType().Name}] no estado [{eve.State}] não pode ser atualizado.");
                }

                // throw new Exception(e.Message);
                throw;
            }
        }



    }
}
