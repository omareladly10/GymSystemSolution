using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymSystem.DAL.Repositories.classes
{
    public class PlanRepository : IPlanRepository
    {

        private readonly GymDbContext dbContext;

        public PlanRepository(GymDbContext _dbContext)

        {

            dbContext = _dbContext;
        }


        public async Task<IEnumerable<Plan>> GetAll(bool isTracked, CancellationToken ct = default)
        {
            var Plans = isTracked ? dbContext.Plans : dbContext.Plans.AsNoTracking();

            return await Plans.ToListAsync();
        }

        

        public async Task<Plan?> GetById(int id, CancellationToken ct = default)
        {
            var Plan = await dbContext.Plans.FirstOrDefaultAsync(x => x.Id == id);

            return Plan;
        }

        public void Update(Plan plan)
        {
          dbContext.Update(plan);

        }


        public void Add(Plan plan)
        {
            dbContext.Add(plan);
        }



        public void Delete(int plan)
        {
            var Plan = dbContext.Plans.FirstOrDefault(p => p.Id == plan);

            if (Plan != null)
                dbContext.Plans.Remove(Plan);
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

       
    }
}
