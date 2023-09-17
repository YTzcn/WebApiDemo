using System.Linq.Expressions;
using Yahya.WebApiDemo.Entities;

namespace Yahya.WebApiDemo.DataAcces
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null); //Filte gelirse filteye göre gelmezse hepsi gelecek
        T GetById(Expression<Func<T, bool>> filter);//Filtre zorunlı
        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
    }
}