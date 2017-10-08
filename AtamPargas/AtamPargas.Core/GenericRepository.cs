using AtamPargas.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AtamPargas.Core
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private AtamPargasDBEntities db = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this.db = new AtamPargasDBEntities();
            table = db.Set<T>();
        }
        public GenericRepository(AtamPargasDBEntities db)
        {
            this.db = db;
            table = db.Set<T>();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> SelectAll()
        {
            return table.ToList();
        }

        public T SelectByID(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
