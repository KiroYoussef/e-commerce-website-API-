using Electronic.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Electronic.Api.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ContextDB _context;
        public BaseRepository(ContextDB context)
        {
            _context = context;
        }


        public T GetById(int Id)
        {
            return _context.Set<T>().Find(Id);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAllwhere(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }




        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public bool CheckAny(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> idproduct)
        {
            return _context.Set<T>().Where(predicate).Any(idproduct);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate);
        }

        public T GetById(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> idproduct)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault(idproduct);
        }

        public T GetByIds(string Id)
        {
            return _context.Set<T>().Find(Id);
        }


        public T GetByFirst(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public string GetNameById(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> idproduct)
        {
            return _context.Set<T>().Where(predicate).Select(idproduct).FirstOrDefault();
        }

    }
}
