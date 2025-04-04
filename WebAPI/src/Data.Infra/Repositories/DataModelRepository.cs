using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain.Models;
using Data.Infra.Context;

namespace Data.Infra.Repositories
{
    public class DataModelRepository : Repository<DataModel>
    {
        public DataModelRepository(AppDbContext context) : base(context)
        {}

        public override DataModel GetById(int id)
        {
            var query = _context.Set<DataModel>().Where(e => e.Id == id);

            if(query.Any())
                return query.First();

            return null;
        }

        public override IEnumerable<DataModel> GetAll()
        {
            var query = _context.Set<DataModel>();

            return query.Any() ? query.ToList() : new List<DataModel>();
        }
    }
}