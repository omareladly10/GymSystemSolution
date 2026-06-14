using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Repositories.classes
{
    public class UnitOfWork : IUnitOfWork
    {

     private readonly Dictionary<string, object> _Repos = [];

     private readonly GymDbContext dbContext;

        public ISessionRepository SessionRepository { get; }

        public UnitOfWork( GymDbContext dbContext)
        {
            this.dbContext = dbContext;

            SessionRepository = new SessionRepository(dbContext);
        }



        

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var TypeNAme = typeof(TEntity).Name;

            if (_Repos.TryGetValue(TypeNAme, out object OldRepository))

                return (IGenericRepository<TEntity>) OldRepository;

            var NewRepository = new GenericRepository<TEntity>(dbContext);


            _Repos[TypeNAme] = NewRepository;

            return NewRepository;
        }



        public async Task<int> CompeleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
