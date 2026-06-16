using GymSystem.BLL._ٍServices.Interfaces;
using GymSystem.BLL.ViewModels.MembersViewModels;
using GymSystem.DAL.Entities;
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
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var tempResult = TempData["Result"];
            var Members = await memberServices.GetAllMembersAsync(ct);

            return View(Members);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model, CancellationToken ct)

        {
            if (!ModelState.IsValid) return View(nameof(Create), model);

            var Result = await memberServices.CreateMemberAsync(model, ct);


            if (Result)
                TempData["Success"] = "Member Create Succesfully";
            else
                TempData["Failed"] = "Failed to Create Member";

            return RedirectToAction(nameof(Index));
        }


      [HttpGet]
        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {

            var member = await memberServices.GetMembersDetailsAsync(id, ct);

            if (member is null)
            {
                TempData["ErroMessage"] = "Member Not Found";

                return RedirectToAction(nameof(Index));

            }

            return View(member);
        }


        [HttpGet]
        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        {

            var healthRecord = await memberServices.GetMemberHealthRecordAsync(id, ct);
            if (healthRecord is null)
            {

                TempData["ErrorMessage"] = " Health Record Not Found";

                return RedirectToAction(nameof(Index));
            }

            return View (healthRecord);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await memberServices.DeleteMemberAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}