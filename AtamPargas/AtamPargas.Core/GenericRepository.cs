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

        public bool CheckIfCodeExists(string code, int? id)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public List<string> GetCodeListByCode(string code)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<T> SelectAll()
        {
            return table.ToList();
        }

        public T SelectByID(int id)
        {
            return table.Find(id);
        }

        public void Update(T obj)
        {
            using (var context = new AtamPargasDBEntities())
            {
                context.Entry(obj).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
