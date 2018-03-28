namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities;
    using Domain.Repoository;
    using Microsoft.EntityFrameworkCore;

    public class EntityFrameworkCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;

        public EntityFrameworkCoreRepository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public TEntity GetById(long id)
        {
            return _entities.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities;
        }
    }
}