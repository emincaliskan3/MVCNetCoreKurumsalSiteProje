using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<T> // Buradaki <T> bu interface in generic yani genel olarak kullanılacağını ifade ediyor
    {
        #region Senkron Metotlar
        // Burada Service classlarla ilgili ortak crud işlemlerini yapabilmemizi sağlayan metotların imzalarını belirliyoruz
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression); // x=>x. .. şeklinde sorgu yazabilmemizi sağlayan metot
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
        #endregion

        #region ASenkron Metotlar
        //Entity framework deki asenkron metotları da repository pattern de kullanmak istersek bu şekilde kullanabiliriz. Aşağıdaki metot imzalarını servis classında kullanmalıyız
        Task<T> FindAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetIncludeAsync(Expression<Func<T, bool>> expression, string table);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task<int> SaveAsync();
        #endregion

    }
}
