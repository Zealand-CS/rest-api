using Microsoft.EntityFrameworkCore;
using ShiftsTrackerRestApi.Models;

namespace ShiftsTrackerRestApi.Managers;


    public class RestContext : DbContext
    {
        public RestContext(DbContextOptions<RestContext> options)
            : base(options) { }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<User> Users { get; set; }
    }
