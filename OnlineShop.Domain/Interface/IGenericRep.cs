using System.Linq.Expressions;

namespace OnlineShop.Domain.Interface
{
    public interface IGenericRep<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Take(int recordNumber);
        TEntity Get(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                 string properties = "");

        void Add(TEntity entityToAdd);
        void Remove(object id);
        void Remove(TEntity entityToDelete);
        void Update(TEntity entity);
    }
}
