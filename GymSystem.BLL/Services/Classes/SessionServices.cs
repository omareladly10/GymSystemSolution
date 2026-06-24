using AutoMapper;
using GymSystem.BLL._ٍServices.Interfaces;
using GymSystem.BLL.Common;
using GymSystem.BLL.ViewModels.SessionsViewModels;
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

        public async Task<Result> CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct = default)
        {
            if (model.EndDate <= model.StartDate) return Result.Validation("End Date Must Be After Start Date");

            if (model.StartDate <=DateTime.Now) return Result.Validation("Start Date Must Be in the future");

            var TrainerRepo = unitOfWork.GetRepository<Trainer>();

            var Trainer = await TrainerRepo.GetById(model.TrainerId , ct);

            if (Trainer is null) return Result.NotFound("Trainer Not Found");

            var CategoryRepo = unitOfWork.GetRepository<Category>();
            var Category = await CategoryRepo.GetById(model.CategoryId , ct);
            if (Category is null) return Result.NotFound("Category Not Found");

            var session = mapper.Map<CreateSessionViewModel , Session>(model);

            var SessionRepo = unitOfWork.GetRepository<Session>();


            SessionRepo.Add(session);

            var rowEffected = await unitOfWork.CompeleteAsync();

            return rowEffected > 0 ? Result.ok() : Result.fail("Failed to create session");
        }

        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct)
        {
           var Sessions = await unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategoryAsync(ct);

            if (!Sessions.Any()) return null;

            Sessions = Sessions.OrderByDescending(x => x.StartDate);


            var MappedSessions = mapper.Map<IEnumerable<Session> , IEnumerable<SessionViewModel>>(Sessions);
       


            foreach( var Session in MappedSessions)
            {

                Session.AvailableSlots = Session.Capacity - await unitOfWork.SessionRepository.GetCountOfBookedSlotAsync(Session.Id , ct);
            }

            return MappedSessions;
        }

        public async Task<IEnumerable<CategorySelectViewModel>> GetCategoriesForDropDownAsync(CancellationToken ct = default)
        {

            var categories = await unitOfWork.GetRepository<Category>().GetAll(false, ct);

            return mapper.Map<IEnumerable<Category>, IEnumerable<CategorySelectViewModel>>(categories);
        }

        public async Task<SessionViewModel?> GetSessionByIdAsync(int sessionId, CancellationToken ct)
        {
            
            var  Session =await unitOfWork.SessionRepository.GetSessionByIdWithTrainerAndCategoryAsync(sessionId, ct);
            
            if (Session == null)
                return null;

            var mappedSession = mapper.Map<Session, SessionViewModel>(Session);

            mappedSession.AvailableSlots = mappedSession.Capacity - await unitOfWork.SessionRepository.GetCountOfBookedSlotAsync(mappedSession.Id, ct);

            return mappedSession;
        }

        public async Task<IEnumerable<TrainerSelectViewModel>> GetTrainersForDropDownAsync(CancellationToken ct = default)
        {
            var Trainer = await unitOfWork.GetRepository<Trainer>().GetAll(false, ct);

            return mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerSelectViewModel>>(Trainer);
        }
    }
}
