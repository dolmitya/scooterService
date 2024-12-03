using System.Linq.Expressions;
using ScooterDataAccess.Entities;

namespace ScooterDataAccess.Repository;

public interface IRepository<T> where T : IBaseEntity
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    T? GetById(int id);
    T? GetById(Guid id);
    T Save(T entity);
    void Delete(T entity);
}