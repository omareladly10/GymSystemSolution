using AutoMapper;
using GymSystem.BLL._ٍServices.Interfaces;
using GymSystem.BLL.ViewModels.SessionViewModels;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL._ٍServices.Classes
{
    public class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct)
        {
           var Sessions = await unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategoryAssync(ct);

            if (!Sessions.Any()) return null;

            Sessions = Sessions.OrderByDescending(x => x.StartDate);


            var MappedSessions = mapper.Map<IEnumerable<Session> , IEnumerable<SessionViewModel>>(Sessions);
       


            foreach( var Session in MappedSessions)
            {

                Session.AvailableSlots = Session.Capacity - await unitOfWork.SessionRepository.GetCountOfBookedSlotAsync(Session.Id , ct);
            }

            return MappedSessions;
        }
    }
}
