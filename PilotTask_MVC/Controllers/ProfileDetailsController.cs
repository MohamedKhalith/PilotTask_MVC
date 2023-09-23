using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PilotTask_MVC.Data;
using PilotTask_MVC.Models;
using PilotTask_MVC.Models.Domain;
using System.Threading.Tasks;

namespace PilotTask_MVC.Controllers
{
    public class ProfileDetailsController : Controller
    {
        private readonly ProfileDbContext mVCDbContext;
        public ProfileDetailsController(ProfileDbContext mVCDbContext)
        {
            this.mVCDbContext = mVCDbContext;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mVCDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProfileData addProfile)
        {
            var profile = new ProfileDetail()
            {
                ProfileId = addProfile.ProfileId,
                FirstName = addProfile.FirstName,
                LastName = addProfile.LastName,
                DateOfBirth = addProfile.DateOfBirth,
                EmailId = addProfile.EmailId,
                PhoneNumber = addProfile.PhoneNumber
            };
            await mVCDbContext.Employees.AddAsync(profile);
            await mVCDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var profiles = await mVCDbContext.Employees.FirstOrDefaultAsync(x => x.ProfileId == id);
            if (profiles != null)
            {
                var viewModel = new UpdateProfileData()
                {
                    ProfileId = profiles.ProfileId,
                    FirstName = profiles.FirstName,
                    LastName = profiles.LastName,
                    DateOfBirth = profiles.DateOfBirth,
                    EmailId = profiles.EmailId,
                    PhoneNumber = profiles.PhoneNumber
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateProfileData model)
        {
            var profiles = await mVCDbContext.Employees.FindAsync(model.ProfileId);
            if (profiles != null)
            {
                profiles.FirstName = model.FirstName;
                profiles.LastName = model.LastName;
                profiles.DateOfBirth = model.DateOfBirth;
                profiles.EmailId = model.EmailId;
                profiles.PhoneNumber = model.PhoneNumber;
                await mVCDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProfileData model)
        {
            var profiles = await mVCDbContext.Employees.FindAsync(model.ProfileId);
            if (profiles != null)
            {
                mVCDbContext.Remove(profiles);
                await mVCDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
