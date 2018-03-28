namespace DomainTest.Repository.ListRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities;
    using Domain.Entities.Speaker;
    using Domain.Repoository;

    public class ListRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly ISet<Speaker> _speakers = new HashSet<Speaker>();

        private int _id;

        public int GetNextId => ++_id;

        public void Add(TEntity entity)
        {
            entity.Id = GetNextId;

            switch (entity)
            {
                case Speaker speaker:
                    _speakers.Add(speaker);
                    break;

                default:
                    throw new NotImplementedException(); 
            }
        }

        public void Update(TEntity entity)
        {
            switch (entity)
            {
                case Speaker speaker:
                    _speakers.Remove(_speakers.SingleOrDefault(x => x.Id == speaker.Id));
                    _speakers.Add(speaker);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public void Delete(TEntity entity)
        {
            switch (entity)
            {
                case Speaker speaker:
                    _speakers.Remove(_speakers.SingleOrDefault(x => x.Id == speaker.Id));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public TEntity GetById(long id)
        {
            if (typeof(TEntity) == typeof(Speaker))
            {
                return _speakers.SingleOrDefault(x => x.Id == id) as TEntity;
            }

            throw new NotImplementedException("Unexpected type.");
        }

        public IEnumerable<TEntity> GetAll()
        {
            if (typeof(TEntity) == typeof(Speaker))
            {
                return _speakers as IEnumerable<TEntity>;
            }

            throw new NotImplementedException("Unexpected type.");
        }
    }
}