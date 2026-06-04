using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Repositories.classes
{
    public class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext dbContext;


        public MemberRepository(GymDbContext dbContext) 
        {
            this.dbContext = dbContext;

        }


        public void Add(Member member)
        {

            dbContext.Members.Add(member);
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
        public void Delete(int id)
        {
            var member = dbContext.Members.FirstOrDefault(p => p.Id == id);

            if (member != null)
                dbContext.Members.Remove(member);
        }

        public async Task<IEnumerable<Member>> GetAll(bool isTracked, CancellationToken ct = default)
        {
            var members = isTracked ? dbContext.Members : dbContext.Members.AsNoTracking();

            return await members.ToListAsync();
        }

        public async Task<Member?> GetById(int id, CancellationToken ct = default)
        {
            var member = await dbContext.Members.FirstOrDefaultAsync(x => x.Id == id);

            return member;
        }

        public void Update(Member member)
        {
            dbContext.Members.Update(member);
        }
    }
}
