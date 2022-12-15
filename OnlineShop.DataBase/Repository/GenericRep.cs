using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Interface;
using System.Linq.Expressions;

namespace OnlineShop.DAL.Repository
{
    internal class GenericRep<TEntity> : IGenericRep<TEntity> where TEntity : class
    {
        private readonly MainContext _context;

        public GenericRep(MainContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entityToAdd)
        {
            _context.Set<TEntity>().Add(entityToAdd);
        }

        public virtual void AddRange(IEnumerable<TEntity> entitiesToAddRange)
        {
            _context.Set<TEntity>().AddRange(entitiesToAddRange);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null,
                                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                 string includedProperty = "")
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            // Include() Method For the related dataTables
            foreach (var prop in includedProperty.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop);
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity Get(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual void Remove(object id)
        {
            TEntity entityToDelete = _context.Set<TEntity>().Find(id);
            Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            if(_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entityToDelete);
            }
            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public IEnumerable<TEntity> Take(int recordNumber)
        {
            return _context.Set<TEntity>().Take(recordNumber);
        }

        public void Update(TEntity entityToUpdate)
        {
            _context.Set<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
