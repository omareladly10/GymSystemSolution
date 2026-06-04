using GymSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Repositories.Interfaces
{
  public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {

        Task<IEnumerable<TEntity>> GetAll(bool isTracked, CancellationToken ct = default);
        Task<TEntity?> GetById(int id, CancellationToken ct = default);

        void Add(TEntity item);
        void Update(TEntity item);
        void Delete(int id);

        Task<int> CompleteAsync();


    }
}
