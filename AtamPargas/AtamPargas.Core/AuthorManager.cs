using AtamPargas.Data;
using AtamPargas.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtamPargas.Core
{
    public class AuthorManager : IGenericRepository<AuthorDto>
    {
        private IGenericRepository<Author> repository = null;
        public AuthorManager()
        {
            this.repository = new GenericRepository<Author>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
                cfg.CreateMap<AuthorDto, Author>();
            });
        }
        public void Delete(object id)
        {
            repository.Delete(id);
            repository.Save();
        }
        public void Insert(AuthorDto obj)
        {
            Author entity = new Author();
            Mapper.Map(obj, entity);
            repository.Insert(entity);
            repository.Save();
        }
        public void Save()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<AuthorDto> SelectAll()
        {
            var result = new List<AuthorDto>();
            var entity = repository.SelectAll();
            return Mapper.Map(entity, result);
        }
        public AuthorDto SelectByID(int id)
        {
            Author entity = repository.SelectByID(id);
            AuthorDto result = new AuthorDto();

            return Mapper.Map(entity, result);
        }
        public void Update(AuthorDto obj)
        {
            Author entity = new Author();
            Mapper.Map(obj, entity);
            repository.Update(entity);
            repository.Save();
        }
        public List<string> GetCodeListByCode(string code)
        {
            using (var context = new AtamPargasDBEntities())
            {
                return context.Authors.Where(x => x.AuthorCode.Contains(code)).Select(z => z.AuthorCode).ToList();
            }
        }
        public bool CheckIfCodeExists(string code, int? id)
        {
            bool result = false;
            using (var context = new AtamPargasDBEntities())
            {
                if (id > 0)
                {
                    result = context.Authors.Any(z => z.AuthorCode == code.ToUpper() && z.AuthorId != id);
                }
                else
                {
                    result = context.Authors.Any(z => z.AuthorCode == code.ToUpper());
                }
            }
            return result;
        }
    }
}
