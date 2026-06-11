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
            var tempResult = TempData["Result"];
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

          var Result =   await memberServices.CreateMemberAsync(model, ct);


            if (Result)
                TempData["Success"] = "Member Create Succesfully";
            else
                TempData["Failed"] = "Failed to Create Member";

            return RedirectToAction(nameof(Index));
        }
    }
}
