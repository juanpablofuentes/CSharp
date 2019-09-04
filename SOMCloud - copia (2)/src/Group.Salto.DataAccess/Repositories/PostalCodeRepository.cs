using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Repositories
{
    public class PostalCodeRepository : BaseRepository<PostalCodes>, IPostalCodeRepository
    {
        public PostalCodeRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public PostalCodes GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public PostalCodes GetByCode(string code)
        {
            return Filter(x => x.PostalCode == code).FirstOrDefault();
        }

        public bool ExistCodeAndCity(string code, int city)
        {
            return Any(x => x.PostalCode == code && x.CityId == city);
        }

        public PostalCodes GetByCity(int city)
        {
            return Filter(x => x.CityId == city).FirstOrDefault();
        }

        public Dictionary<int, string> GetAllKeyValuesByMunicipality(int municipalityId)
        {
            var result = Filter(x => x.IdNavigation.MunicipalityId == municipalityId, GetIncludeCitiesAndMunicipalities())
                .OrderBy(o => o.PostalCode)
                .ToDictionary(t => t.Id, t => t.PostalCode);

            return result;
        }

        private List<Expression<Func<PostalCodes, object>>> GetIncludeCitiesAndMunicipalities()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(Cities), typeof(Municipalities) });
        }

        private List<Expression<Func<PostalCodes, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<PostalCodes, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(Cities)) includesPredicate.Add(p => p.IdNavigation);
                if (element == typeof(Municipalities)) includesPredicate.Add(p => p.IdNavigation.Municipality);
            }
            return includesPredicate;
        }
    }
}