using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactEF.Core.Repositories
{
    public class Repository<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbSet<T> _entities;
        private readonly Context _context;
        public Repository(Context context)
        {
            _context = context;
            _entities = _context.Set<T>();

        }

        public async Task Delete(TKey id)
        {
            T tmpEntity = _entities.Find(id);
            if (tmpEntity == null)
            {
                throw new Exception("Entity not found");
            }
            try
            {
                if (_context.Entry(tmpEntity).State == EntityState.Deleted)
                {
                    _entities.Attach(tmpEntity);
                }
                _entities.Remove(tmpEntity);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await _entities.ToListAsync();
        }

        public async virtual Task<T> GetOne(TKey id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> Post(T entity)
        {
            try
            {
                var state = await _entities.AddAsync(entity);
                var res = await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }

        }

        public async Task Put(TKey id, T entity)
        {
            try
            {
                _entities.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
