﻿using CeoHelper.Data.Data.Entities;
using CeoHelper.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CeoHelper.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Request> Requests { get; set; }
    }
}