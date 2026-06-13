using GymSystem.BLL._ٍServices.Interfaces;
using GymSystem.BLL.ViewModels.MembersViewModels;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL._ٍServices.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork unitOfWork;



        //Get

        public MemberServices(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default)
        {
            var members = await unitOfWork.GetRepository<Member>().GetAll(false, ct);
            if (!members.Any()) return [];
            var MemberViewModel = members.Select(m => new MemberViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Photo = m.Photo,
                Gender = m.Gender.ToString()


            });

            return MemberViewModel;
        }

        public async Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int memberId, CancellationToken ct = default)
        {

            var Record = await unitOfWork.GetRepository<HealthRecord>().FirstOrDefultAsync(r=>r.MemberId == memberId , false,ct);

            if(Record is null) return null;

            return new HealthRecordViewModel()
            {
                Weight = Record.Weight,
                Height = Record.Height,
                BloodType = Record.BloodType,
                Note = Record.Note,
            };
           
        }

        public async Task<MemberViewModel?> GetMembersDetailsAsync(int memberId, CancellationToken ct = default)
        {

            // Member + MemberShip + Plan 

            var member = await unitOfWork.GetRepository<Member>().GetById(memberId, ct);
            if (member == null) return null;

            var MemberVM = new MemberViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Gender = member.Gender.ToString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}"
            };

            //MemberShip

            var ActiveMemberShip = await unitOfWork.GetRepository<Membership>().FirstOrDefultAsync(mb => mb.MemberId == memberId && mb.EndDate > DateTime.Now, false, ct);


            if (ActiveMemberShip is not null)
            {
                var ActivePlan = await unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId, ct);

                MemberVM.PlanName = ActivePlan?.Name;
                MemberVM.MembershipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                MemberVM.MembershipEndDate = ActiveMemberShip.EndDate.ToShortDateString();

            }
            return MemberVM;
        }



        public async Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId, CancellationToken ct = default)
        {

            var member = await unitOfWork.GetRepository<Member>().GetById(memberId, ct);

            if (member is null) return null;

            return new MemberToUpdateViewModel()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Street = member.Address.Street,
                City = member.Address.City,
                BuildingNumber = member.Address.BuildingNumber,
                Photo = member.Photo,


            };


       
        }

      //post

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
           var emailExsists = await unitOfWork.GetRepository<Member>().AnyAsync(m=>m.Email == model.Email , ct);
            var phoneExsists = await unitOfWork.GetRepository<Member>().AnyAsync(m => m.Phone == model.Phone, ct);


            if (emailExsists || phoneExsists) return false;

            var Member = new Member()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    Street = model.Street,
                    City = model.City,
                },
                HealthRecord = new HealthRecord()
                {
                    Weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height,
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note,
                }
            };

            unitOfWork.GetRepository<Member>().Add(Member);

            var Result = await unitOfWork.CompeleteAsync();

            return Result > 0;

        }
        public async Task<bool> UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {

            var member = await unitOfWork.GetRepository<Member>().GetById(id, ct);

            if (member is null) return false;

            if(await unitOfWork.GetRepository<Member>().AnyAsync(m=>m.Email == model.Email && m.Id != id)) return false;

            if (await unitOfWork.GetRepository<Member>().AnyAsync(m => m.Phone == model.Phone && m.Id != id)) return false;


            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.City = model.City;
            member.Address.Street = model.Street;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.UpdatedAt = DateTime.Now;

            unitOfWork.GetRepository<Member>().Update (member);

            var Result = await unitOfWork.CompeleteAsync();

            return Result > 0;

        }
        public async Task<bool> DeleteMemberAsync(int memberId, CancellationToken ct = default)
        {
         
            var HasFutureSession = await unitOfWork.GetRepository<Booking>().AnyAsync(b=>b.MemberId == memberId && b.Session.EndDate > DateTime.Now);

            if (HasFutureSession) return false;

            unitOfWork.GetRepository<Member>().Delete(memberId);

            var Result = await unitOfWork.CompeleteAsync();
            
            return Result > 0;
            
        }
    }
}
