using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Domain.Interfaces;
using Data.Domain.Models;
using Data.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        
        public virtual TEntity GetById(int id)
        {
            var query = _context.Set<TEntity>().Where(e => e.Id == id);

            if(query.Any())
                return query.FirstOrDefault();

            return null;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = _context.Set<TEntity>();

            if(query.Any())
                return query.ToList();

            return new List<TEntity>();
        }

        public async Task<bool> Save(IEnumerable<DataModel> entitys)
        {
            foreach (var entity in entitys.ToList())
            {

                var data =  await _context.DataModel.FirstOrDefaultAsync(index => index.Year == entity.Year && index.Description == entity.Description);

                if (data != null)
                {
                    entity.Id = data.Id;
                    await UpdateData(entity);
                }
                else
                {
                    await _context.Set<DataModel>().AddAsync(entity);
                }

            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<DataModel> UpdateData(DataModel obj)
        {
            var data = await _context.DataModel.FirstOrDefaultAsync(index => index.Year == obj.Year && index.Description == obj.Description);
            if (data != null)
            {
                data.Year = obj.Year;
                data.Description = obj.Description;

                var result = await _context.SaveChangesAsync();
                return result >= 0 ? data : null;
            }
            return null;
        }
    }
}