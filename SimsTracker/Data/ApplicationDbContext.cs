using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SimsTracker.Models;

namespace SimsTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SimsTracker.Models.Sim> Sim { get; set; }
        public DbSet<SimsTracker.Models.Hood> Hood { get; set; }
    }
}
