using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planner_Application.Models
{
    //this class helps us interact with the database
    public class PlannerApplicationDBContext : DbContext
    {
        public PlannerApplicationDBContext(DbContextOptions<PlannerApplicationDBContext> options) : base(options)
        {
        }

        //DbSet objects created in order to help interact with database
        public virtual DbSet<TaskViewModel> TaskDB { get; set; }
        public virtual DbSet<EventViewModel> EventDB { get; set; }
    }
}
