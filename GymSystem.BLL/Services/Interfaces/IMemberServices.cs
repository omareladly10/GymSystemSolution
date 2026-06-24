using GymSystem.BLL.ViewModels.MembersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL._ٍServices.Interfaces
{
    public interface IMemberServices
    {
        //get Model -> ViewModel -> view

        Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken ct = default);
        Task<MemberViewModel?> GetMembersDetailsAsync(int memberID , CancellationToken ct = default);


        Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int memberId , CancellationToken ct = default);
        Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId , CancellationToken ct = default);

        //post ViewModel - > MOdel -> DB



        Task<bool> CreateMemberAsync (CreateMemberViewModel model,  CancellationToken ct = default);

        Task<bool>UpdateMemberDetailsAsync(int id ,  MemberToUpdateViewModel model, CancellationToken ct = default);

        Task<bool> DeleteMemberAsync (int memberId , CancellationToken ct = default); 
    }
}
