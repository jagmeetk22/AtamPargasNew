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
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(AuthorDto obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthorDto> SelectAll()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
            });
            var result = new List<AuthorDto>();
            var entity = repository.SelectAll();
            return Mapper.Map(entity, result);
        }

        public AuthorDto SelectByID(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(AuthorDto obj)
        {
            throw new NotImplementedException();
        }
    }
}
