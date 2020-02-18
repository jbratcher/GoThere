using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GoThere.Models;
using GoThere.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace GoThere.Data
{
    public class ApplicationDbContext : IdentityDbContext<GoThereUser, IdentityRole<Guid>, Guid>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Location> Location { get; set; }

        public DbSet<GoThereUser> GoThereUsers { get; set; }

    }
}
