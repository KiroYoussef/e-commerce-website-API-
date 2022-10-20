using System.Linq.Expressions;

namespace Electronic.Api.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int Id);
        T GetByIds(string Id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        bool CheckAny(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> idproduct);
        int Count(Expression<Func<T, bool>> predicate);
        T GetById(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> idproduct);
        IEnumerable<T> GetAllwhere(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
        public T GetByFirst(Expression<Func<T, bool>> predicate);
        string GetNameById(Expression<Func<T, bool>> predicate, Expression<Func<T, string>> idproduct);
    }
}
