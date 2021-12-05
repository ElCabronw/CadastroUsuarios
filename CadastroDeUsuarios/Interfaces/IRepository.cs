using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CadastroDeUsuarios.Interfaces
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IRepository
    {
        void Adicionar(TEntity obj);
        IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
       
        IEnumerable<TEntity> ObterTodos();
        void Remover(long id);
    }
}
