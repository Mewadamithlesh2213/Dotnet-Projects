using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MywebApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {


        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            //_context.products.Include(x=>x.Category)
            _dbset = _context.Set<T>();
        }





        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbset.RemoveRange(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate=null, string? includeProperties=null)
        {
            IQueryable<T> query = _dbset;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);

                }
             }
            return query.ToList(); 
        }

        public T GetT(Expression<Func<T, bool>> predicate, string? includeProperties=null)
        {

            IQueryable<T> query = _dbset;
            query = query.Where(predicate);

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);

                }
            }
            return query.FirstOrDefault();
        }
    }
}
