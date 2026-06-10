using GymSystem.BLL._ٍServices.Interfaces;
using GymSystem.BLL.ViewModels.MembersViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Controllers
{
    public class MemberController : Controller
    {

        private readonly IMemberServices memberServices;

        public MemberController(IMemberServices memberServices)
        {
            this.memberServices = memberServices;
            
        }
        public async Task <IActionResult> Index(CancellationToken ct)
        {
            var Members = await memberServices.GetAllMembersAsync(ct);

            return View(Members);
        }

        [HttpGet]
        public IActionResult Create ()
        { 
        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>CreateMember(CreateMemberViewModel model , CancellationToken ct)

        {
            if (!ModelState.IsValid) return View(nameof(Create), model);

            await memberServices.CreateMemberAsync(model, ct);

            return RedirectToAction(nameof(Index));
        }
    }
}
