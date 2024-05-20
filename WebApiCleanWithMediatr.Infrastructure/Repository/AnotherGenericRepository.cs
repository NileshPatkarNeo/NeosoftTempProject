using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCleanWithMediatr.Infrastructure.Repository
{
    public interface IGenericRepository<T>
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        List<T> Read(string include1 = "", string include2 = "");
    }

    public class AnotherGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _Mcontext;

        internal DbSet<T> _dbSet;

        public AnotherGenericRepository(ApplicationDbContext Context)
        {
            _Mcontext = Context;
            _dbSet = _Mcontext.Set<T>();
        }

        public bool Create(T entity)//(Hospital hospital)
        {
            bool IsCreated = false;
            _Mcontext.Add(entity);
            _Mcontext.SaveChanges();
            IsCreated = true;

            return IsCreated;
        }

        public List<T> Read(string include1 = "", string include2 = "")
        {
            if (string.IsNullOrWhiteSpace(include1) && string.IsNullOrWhiteSpace(include2))
            {
                List<T> result = _dbSet.ToList();
                return result;                    //LazyLoading
            }
            else if (!string.IsNullOrWhiteSpace(include1) && string.IsNullOrWhiteSpace(include2))
            {
                List<T> result = _dbSet.Include(include1).ToList();    //EagerLoading
                return result;
            }
            else if (!string.IsNullOrWhiteSpace(include2) && string.IsNullOrWhiteSpace(include1))
            {
                List<T> result = _dbSet.Include(include2).ToList();    //EagerLoading
                return result;
            }
            else
            {
                List<T> result = _dbSet.Include(include2).Include(include1).ToList();    //EagerLoading
                return result;
            }

            //List<T> result = _dbSet.ToList();
            //return result;
        }



        public bool Update(T entity)
        {
            bool IsUpdated = false;
            _Mcontext.Update(entity);
            _Mcontext.SaveChanges();
            IsUpdated = true;
            return IsUpdated;
        }

        public bool Delete(T entity)
        {
            bool IsDeleted = false;
            _Mcontext.Remove(entity);
            _Mcontext.SaveChanges();
            IsDeleted = true;
            return IsDeleted;
        }

        //public List<T> ReadByUserId(string userId)
        //{
        //    return _Mcontext.Set<T>().Where(item => item.Id == userId).ToList();

        //}

        public void Dispose()
        {

        }
    }
}
