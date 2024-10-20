﻿using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Service
{
    public class Service<T> : IService<T> where T : class // Buraya gönderilecek olan T bir class olmalıdır
        , IEntity // gönderilecek olan bu class IEntity den miras almış olmalıdır
        , new() // ve (Product product = new Product() şeklinde) new lenebilir bir tip olmalıdır
    {
        private readonly DatabaseContext _context; // tabloları tutan contextimiz
        private readonly DbSet<T> _dbSet; // veritabanı işlemlerini yürüteceğimiz dbset
        public Service(DatabaseContext context)
        {
            _context = context; // yukardaki boş contexti doldurduk
            _dbSet = _context.Set<T>(); // yukardaki boş db seti hangi class gönderilmişse onun için çalışmak üzere ayarladık
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            // await _context.AddAsync(entity);  // gelen nesneyi direk context üzerinden ekleyebiliriz
            await _dbSet.AddAsync(entity); // veya dbset üzerinden de ekleyebiliriz.
        }

        public void Delete(T entity)
        {

            _dbSet.Remove(entity);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            // return await _dbSet.Where(expression).FirstOrDefaultAsync();
            return await _dbSet.FirstOrDefaultAsync(expression); // 2. yöntem
        }

        public async Task<T> GetIncludeAsync(Expression<Func<T, bool>> expression, string table)
        {
            return await _dbSet.Where(expression).Include(table).FirstOrDefaultAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
