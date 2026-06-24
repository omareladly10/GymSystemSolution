using GymSystem.BLL.Common;
using GymSystem.BLL.ViewModels.SessionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL._ٍServices.Interfaces
{
    public interface ISessionServices
    {
        public Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct);

        Task<Result> CreateSessionAsync(CreateSessionViewModel model , CancellationToken ct = default);


        Task<IEnumerable<TrainerSelectViewModel>> GetTrainersForDropDownAsync(CancellationToken ct = default);

        Task<IEnumerable<CategorySelectViewModel>> GetCategoriesForDropDownAsync(CancellationToken ct = default);


        Task<SessionViewModel?> GetSessionByIdAsync(int sessionId, CancellationToken ct );



        Task<UpdateSessionViewModel>GetSessionToUpdateAsync(int sessionId, CancellationToken ct );


        Task<Result> UpdateSessionAsync(int id ,UpdateSessionViewModel model, CancellationToken ct = default);
    }
}
