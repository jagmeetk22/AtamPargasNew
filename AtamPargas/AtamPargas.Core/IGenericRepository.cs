using System.Collections.Generic;

namespace AtamPargas.Core
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectByID(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        List<string> GetCodeListByCode(string code);
        bool CheckIfCodeExists(string code,int? id);
    }

}
