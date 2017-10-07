using AtamPargas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtamPargas.Data;
using AutoMapper;

namespace AtamPargas.Core
{
    public class SubjectManager
    {

        public List<T> GetAll<T>() where T : class
        {
            using (var ctx = new AtamPargasDBEntities())
            {
               return ctx.Set<T>().ToList();
            };
        }
        //public List<T> GetAllSubject<Y,T>() where T : class where Y : class
        //{
        //    using (var ctx = new AtamPargasDBEntities())
        //    {
        //        Mapper.Initialize(cfg =>
        //        {
        //            cfg.CreateMap<Y, T>();
        //        });
        //        var result = new List<T>();
        //        var entity = GetAll<Y>();
        //        return Mapper.Map(entity, result);
        //    };
        //}
        public List<T> GetAllSubject<T>() where T : class
        {
            using (var ctx = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Subject, T>();
                });
                var result = new List<T>();
                var entity = GetAll<Subject>();
                return Mapper.Map(entity, result);
            };
        }
        public List<SubjectDto> AllSubjects()
        {
            using (var context = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Subject, SubjectDto>();
                });
                var result = new List<SubjectDto>();
                var subjects = context.Subjects.ToList();
                return Mapper.Map(subjects, result);
            }
        }
        public List<SubjectDto> ParentSubjects()
        {
            using (var context = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Subject, SubjectDto>();
                });
                var result = new List<SubjectDto>();
                var subjects = context.Subjects.Where(x => x.IsSubSubject == false).OrderBy(x=>x.SubjectName).ToList();
                return Mapper.Map(subjects, result);
            }
        }
        public List<SubjectDto> SubSubjects()
        {
            using (var context = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Subject, SubjectDto>();
                });
                var result = new List<SubjectDto>();
                var subjects = context.Subjects.Where(x => x.IsSubSubject == true).OrderBy(x=>x.SubjectName).ToList();
                return Mapper.Map(subjects, result);
            }
        }
        public bool Add(SubjectDto model)
        {
            using (var context = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<SubjectDto, Subject>();
                });

                var entity = new Subject();
                Mapper.Map(model, entity);

                context.Subjects.Add(entity);
                int count = context.SaveChanges();
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Tuple Item 1 : if true then deleted successfully
        /// Tuple Item 2 : if true then it is sub subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tuple<bool,bool> Delete(int id)
        {
            Tuple<bool, bool> result = new Tuple<bool, bool>(false,false);
            using (var context = new AtamPargasDBEntities())
            {
                var entity = context.Subjects.FirstOrDefault(x => x.SubjectId == id);

                if(entity.IsSubSubject)
                {
                    context.Subjects.Remove(entity);
                }
                else
                {
                    var subjectHaveSubSubjects = CheckIfSubjectHaveSubSubjects(id);
                    if(!subjectHaveSubSubjects)
                    {
                        context.Subjects.Remove(entity);
                    }
                }

                int count = context.SaveChanges();
                if (count > 0)
                {
                    result = new Tuple<bool, bool>(true, entity.IsSubSubject);
                    return result;
                }

            }
            return result;
        }
        public SubjectDto GetById(int id)
        {
            using (var context = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Subject, SubjectDto>();
                });
                var entity = context.Subjects.FirstOrDefault(x => x.SubjectId == id);
                var result = new SubjectDto();
                return Mapper.Map(entity, result);
            }
        }
        public bool UpdateById(SubjectDto model)
        {
            using (var context = new AtamPargasDBEntities())
            {
                var entity = context.Subjects.FirstOrDefault(x => x.SubjectId == model.SubjectId);
                entity.SubjectCode = model.SubjectCode;
                entity.SubjectName = model.SubjectName;
                entity.SubjectNamePunjabi = model.SubjectNamePunjabi;
                entity.SubjectDescription = model.SubjectDescription;
                entity.ModifiedOn = DateTime.Now;
                int count = context.SaveChanges();
                if(count>0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<SubjectDto> GetSubSubjectsOfSubjectById(int id)
        {
            using (var context = new AtamPargasDBEntities())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Subject, SubjectDto>();
                });
                var entity = context.Subjects.Where(x => (x.ParentSubjectId==0 || x.ParentSubjectId==id) && x.IsSubSubject==true && !x.IsDeleted).OrderBy(x => x.SubjectName).ToList();
                var result = new List<SubjectDto>();
                return Mapper.Map(entity, result);
            }
        }
        public bool UpdateSubSubjectOfSubject(int parentSubjectId, int subSubjectId, bool checkBoxChecked)
        {
            using (var context = new AtamPargasDBEntities())
            {
                var entity = context.Subjects.FirstOrDefault(x => x.SubjectId == subSubjectId);

                if (checkBoxChecked)
                {
                    entity.ParentSubjectId = parentSubjectId;
                }
                else
                {
                    entity.ParentSubjectId = 0;
                }
                int count = context.SaveChanges();
                if (count > 0)
                {
                    return true;
                }
            }
                return false;
        }
        public List<string> GetSubjectCodeListByCode(string code)
        {
            using (var context = new AtamPargasDBEntities())
            {
                return context.Subjects.Where(x => x.SubjectCode.Contains(code)).Select(z=>z.SubjectCode).ToList();
            }
        }
        public bool CheckIfCodeExists(string code)
        {
            using (var context = new AtamPargasDBEntities())
            {
                return context.Subjects.Any(z => z.SubjectCode==code);
            }
        }
        public bool CheckIfSubjectHaveSubSubjects(int id)
        {
            using (var context = new AtamPargasDBEntities())
            {
                return context.Subjects.Any(x => x.ParentSubjectId == id);
            }
        }

    }
}
