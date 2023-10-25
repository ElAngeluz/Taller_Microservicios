using microscore.application.interfaces.abstractapp;
using microscore.domain.entities.abstractDomain;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Dynamic.Core;

namespace microscore.infrastructure.abstractInfra
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class, IEntity
    {
        private readonly DbContext _dbContext;

        public GenericRepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                Log.Information("Consulta de entidad por identificacion.");
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "error al consultar la entidad");
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetPagedAdvancedReponseAsync(int pageNumber, int pageSize, string orderBy, string fields)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select<T>("new(" + fields + ")")
                .OrderBy(orderBy)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            try
            {
                Log.Information("Se procede con el ingreso de la entidad");
                entity.State = true;
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception Ex)
            {
                Log.Fatal(Ex, "Se produjo un error al obtener todos los registros de la entidad.");
                throw;
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            Log.Information("Se procede con el actualizacion de la entidad");
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            Log.Information("Se procede con la eliminacion de la entidad");
            entity.State = false;
            await UpdateAsync(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(bool state = true)
        {
            Log.Information("Se procede con la consulta de las entidades.");

            return state ? await _dbContext
                 .Set<T>()
                 .Where(c => c.State == state)
                 .ToListAsync()
                 : await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }
    }
}