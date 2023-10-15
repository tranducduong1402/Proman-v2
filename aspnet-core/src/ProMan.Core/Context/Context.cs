using Abp;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.Context
{
    public class Context : AbpServiceBase, IContext
    {
        private readonly IIocManager _iocManager;
        public Context(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        IQueryable<TEntity> IContext.GetAll<TEntity, TPrimaryKey>()
        {
            return (this as IContext).GetRepo<TEntity, TPrimaryKey>().GetAll();
        }

        IQueryable<TEntity> IContext.GetAll<TEntity>()
        {
            return (this as IContext).GetRepo<TEntity, long>().GetAll();
        }

        IQueryable<TEntity> IContext.All<TEntity>()
        {
            return (this as IContext).GetRepo<TEntity, long>().GetAll();
        }

        IRepository<TEntity, TPrimaryKey> IContext.GetRepo<TEntity, TPrimaryKey>()
        {
            var repoType = typeof(IRepository<,>);
            Type[] typeArgs = { typeof(TEntity), typeof(TPrimaryKey) };
            var repoGenericType = repoType.MakeGenericType(typeArgs);
            var resolveMethod = _iocManager.GetType()
                .GetMethods()
                .First(s => s.Name == "Resolve" && !s.IsGenericMethod && s.GetParameters().Length == 1 && s.GetParameters()[0].ParameterType == typeof(Type));
            var repo = resolveMethod.Invoke(_iocManager, new object[] { repoGenericType });
            return repo as IRepository<TEntity, TPrimaryKey>;
        }

        IRepository<TEntity, long> IContext.GetRepo<TEntity>()
        {
            return (this as IContext).GetRepo<TEntity, long>();
        }

        IRepository<TEntity, long> IContext.Repository<TEntity>()
        {
            return (this as IContext).GetRepo<TEntity, long>();
        }

        TEntity IContext.Clone<TEntity>(TEntity entity)
        {
            entity.Id = 0;
            return (this as IContext).GetRepo<TEntity, long>().Insert(entity);
        }

        long IContext.CloneAndGetId<TEntity>(TEntity entity)
        {
            entity.Id = 0;
            return (this as IContext).GetRepo<TEntity, long>().InsertAndGetId(entity);
        }

        IEnumerable<TEntity> IContext.InsertRange<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                yield return (this as IContext).GetRepo<TEntity, long>().Insert(entity);
            }
        }

        async Task<IEnumerable<TEntity>> IContext.InsertRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        {
            var updatedEntities = new List<TEntity>();
            foreach (var entity in entities)
            {
                updatedEntities.Add(await (this as IContext).GetRepo<TEntity, long>().InsertAsync(entity));
            }

            return updatedEntities;
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return (this as IContext).GetRepo<TEntity, long>().Insert(entity);
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return await (this as IContext).GetRepo<TEntity, long>().InsertAsync(entity);
        }

        public long InsertAndGetId<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return (this as IContext).GetRepo<TEntity, long>().InsertAndGetId(entity);
        }

        public async Task<long> InsertAndGetIdAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return await (this as IContext).GetRepo<TEntity, long>().InsertAndGetIdAsync(entity);
        }


        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return (this as IContext).GetRepo<TEntity, long>().Update(entity);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return await (this as IContext).GetRepo<TEntity, long>().UpdateAsync(entity);
        }

        public long InsertOrUpdateAndGetId<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return (this as IContext).GetRepo<TEntity, long>().InsertOrUpdateAndGetId(entity);
        }

        public async Task<long> InsertOrUpdateAndGetIdAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            return await (this as IContext).GetRepo<TEntity, long>().InsertOrUpdateAndGetIdAsync(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            (this as IContext).GetRepo<TEntity, long>().Delete(entity);
        }

        public void Delete<TEntity>(long id) where TEntity : class, IEntity<long>
        {
            (this as IContext).GetRepo<TEntity, long>().Delete(id);
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<long>
        {
            await (this as IContext).GetRepo<TEntity, long>().DeleteAsync(entity);
        }   

        public async Task DeleteAsync<TEntity>(long id) where TEntity : class, IEntity<long>
        {
            await (this as IContext).GetRepo<TEntity, long>().DeleteAsync(id);
        }

        public void SoftDelete<TEntity>(TEntity entity) where TEntity : class, IEntity<long>, ISoftDelete
        {
            entity.IsDeleted = true;
            (this as IContext).GetRepo<TEntity, long>().Update(entity);
        }

        public async Task SoftDeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<long>, ISoftDelete
        {
            entity.IsDeleted = true;
            await (this as IContext).GetRepo<TEntity, long>().UpdateAsync(entity);
        }

        public TEntity Get<TEntity>(long id) where TEntity : class, IEntity<long>
        {
            return (this as IContext).GetRepo<TEntity, long>().Get(id);
        }

        public async Task<TEntity> GetAsync<TEntity>(long id) where TEntity : class, IEntity<long>
        {
            return await (this as IContext).GetRepo<TEntity, long>().GetAsync(id);
        }

        IEnumerable<TEntity> IContext.UpdateRange<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                yield return (this as IContext).GetRepo<TEntity, long>().Update(entity);
            }
        }

        async Task<IEnumerable<TEntity>> IContext.UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities)
        {
            var updatedEntities = new List<TEntity>();
            foreach (var entity in entities)
            {
                updatedEntities.Add(await (this as IContext).GetRepo<TEntity, long>().UpdateAsync(entity));
            }   

            return updatedEntities;
        }

        Task<IEnumerable<TEntityDto>> IContext.Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TEntityDto>> IContext.Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input, Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<TEntityDto>> IContext.Sync<TEntityDto, TEntity>(IEnumerable<TEntityDto> input, Expression<Func<TEntity, bool>> condition, Func<TEntityDto, TEntityDto> merge)
        {
            throw new NotImplementedException();
        }

        Task IContext.DeleteRangeAsync<TEntity>(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
