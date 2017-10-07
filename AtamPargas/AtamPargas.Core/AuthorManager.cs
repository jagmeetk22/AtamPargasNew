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
    public class AuthorManager
    {
        public List<AuthorDto> AllAuthors()
        {
            using (var context = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Author, SubjectDto>();
                });
                var result = new List<AuthorDto>();
                var entity = context.Authors.ToList();
                return Mapper.Map(entity, result);
            }
        }

           

    }
}
