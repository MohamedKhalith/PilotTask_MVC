using Microsoft.EntityFrameworkCore;
using PilotTask_MVC.Models.Domain;

namespace PilotTask_MVC.Data
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProfileDetail> Employees { get; set; }
        public DbSet<TaskDetail> TaskDetailDb { get; set; }
    }
}
