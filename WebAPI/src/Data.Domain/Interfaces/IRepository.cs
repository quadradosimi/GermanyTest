using Data.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
         TEntity GetById(int id);
         IEnumerable<TEntity> GetAll();
         Task<bool> Save(IEnumerable<DataModel> entitys);
    }
}