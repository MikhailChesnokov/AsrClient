namespace Domain.Repoository
{
    using System.Collections.Generic;
    using Entities;

    public interface IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        TEntity GetById(long id);

        IEnumerable<TEntity> GetAll();
    }
}