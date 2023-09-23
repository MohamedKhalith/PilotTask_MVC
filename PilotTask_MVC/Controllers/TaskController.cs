using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PilotTask_MVC.Data;
using PilotTask_MVC.Models;
using PilotTask_MVC.Models.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PilotTask_MVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ProfileDbContext mVCDbContext;
        public TaskController(ProfileDbContext mVCDbContext)
        {
            this.mVCDbContext = mVCDbContext;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var taskDetails = await mVCDbContext.TaskDetailDb.Where(x => x.ProfileId == id).ToListAsync();
                return View(taskDetails);
            }
            catch(Exception ex)
            {
                //have to to handle Logging but this time just print the console.
                Console.WriteLine($"Exception Occured{ex}");
                return RedirectToAction("Index", "ProfileDetails");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateTaskData addTask)
        {
            try
            {
                //Need to move mapping class
                var profile = new TaskDetail()
                {
                    ProfileId = addTask.ProfileId,
                    TaskName = addTask.TaskName,
                    TaskDescription = addTask.TaskDescription,
                    StartTime = addTask.StartTime,
                    Status = addTask.Status,
                };
                await mVCDbContext.TaskDetailDb.AddAsync(profile);
                await mVCDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "ProfileDetails");
            }
            catch(Exception ex)
            {
                //have to to handle Logging but this time just print the console.
                Console.WriteLine($"Exception Occured{ex}");
                return RedirectToAction("Index", "ProfileDetails");
            }
            
        }
        [HttpGet]
        [Route("Task/Index/Task/View/{id}")]
        public async Task<IActionResult> View(int id)
        {
            try
            {
                var profiles = await mVCDbContext.TaskDetailDb.FirstOrDefaultAsync(x => x.ProfileId == id);
                if (profiles != null)
                {
                    var viewModel = new UpdateTaskData()
                    {
                        ProfileId = profiles.ProfileId,
                        TaskDescription = profiles.TaskDescription,
                        TaskName = profiles.TaskName,
                        Status = profiles.Status,
                        StartTime = profiles.StartTime
                    };
                    return await Task.Run(() => View("View", viewModel));
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //have to to handle Logging but this time just print the console.
                Console.WriteLine($"Exception Occured{ex}");
                return RedirectToAction("Index", "ProfileDetails");
            }
            
        }
        [HttpPost]
        [Route("Task/Index/Task/View/View")]
        public async Task<IActionResult> View(UpdateTaskData model)
        {
            try
            {
                var profiles = await mVCDbContext.TaskDetailDb.FirstOrDefaultAsync(x => x.ProfileId == model.ProfileId);
                if (profiles != null)
                {
                    profiles.StartTime = model.StartTime;
                    profiles.Status = model.Status;
                    profiles.TaskName = model.TaskName;
                    profiles.TaskDescription = model.TaskDescription;
                    profiles.ProfileId = model.ProfileId;
                    await mVCDbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "ProfileDetails");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //have to to handle Logging but this time just print the console.
                Console.WriteLine($"Exception Occured{ex}");
                return RedirectToAction("Index", "ProfileDetails");
            }

        }
        

        [HttpGet]
        [Route("Task/Index/Task/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employees = await mVCDbContext.TaskDetailDb.FindAsync(id);
                if (employees != null)
                {
                    mVCDbContext.Remove(employees);
                    await mVCDbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "ProfileDetails");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //have to to handle Logging but this time just print the console.
                Console.WriteLine($"Exception Occured{ex}");
                return RedirectToAction("Index", "ProfileDetails");
            }

        }

    }
}
