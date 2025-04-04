using System.Threading.Tasks;

namespace Data.Domain.Interfaces
{
    public interface IUnitOfWork
    {
          Task Commit();
    }
}