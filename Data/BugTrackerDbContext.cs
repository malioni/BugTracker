using System;
using BugTracker.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BugTracker.Data
{
	public class BugTrackerDbContext: DbContext
	{
		public DbSet<Bug> Bugs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Note> Notes { get; set; }
		public string DbPath { get; set; }

        public BugTrackerDbContext()
		{
			DbPath = "bug_tracker.db";
		}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
}

