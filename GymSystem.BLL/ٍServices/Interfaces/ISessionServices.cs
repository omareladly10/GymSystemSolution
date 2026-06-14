using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.BLL._ٍServices.Interfaces
{
    public interface ISessionServices
    {
        public Task<IEnumerable<>> GetAllSessionsAsync(CancellationToken ct);

    }
}
