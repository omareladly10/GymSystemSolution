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
    public class MemberRepository :GenericRepository<Member> , IMemberRepository
    {
        private readonly GymDbContext dbContext;


        public MemberRepository(GymDbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;

        }


       
    }
}
