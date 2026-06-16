using GymSystem.BLL._ٍServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymSystem.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionServices sessionServices;

        public SessionController(ISessionServices sessionServices)
        {
            this.sessionServices = sessionServices;
        }
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var Session = await sessionServices.GetAllSessionsAsync(ct);
            return View(Session);
        }
    }
}
