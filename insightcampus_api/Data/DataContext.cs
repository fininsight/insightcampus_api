﻿using System;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;

namespace insightcampus_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<CategoryModel> CategoryContext { get; set; }
        public DbSet<RoleModel> RoleContext { get; set; }
        public DbSet<UserModel> UserContext { get; set; }
    }
}
