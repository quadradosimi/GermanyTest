using Data.Domain.Interfaces;

namespace Data.Domain.Models
{
    public class DataModelService
    {
        private readonly IRepository<DataModel> _dataRepository;

        public DataModelService(IRepository<DataModel> dataRepository)
        {
            _dataRepository = dataRepository;
        }
    }
}