using Data.Domain.Interfaces;
using Data.Domain.Models;
using Data.Infra.Context;
using Data.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Application.DI {
    public class Initializer {
        public static void Configure(IServiceCollection services, string conection)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conection));

            //services
            services.AddScoped(typeof(IRepository<DataModel>), typeof(DataModelRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(DataModelService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}