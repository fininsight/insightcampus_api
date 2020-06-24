using System;
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
            modelBuilder.Entity<CodeModel>().HasKey(role => new
            {
                role.code_id,
                role.codegroup_id
            });
        }

        public DbSet<CategoryModel> CategoryContext { get; set; }
        public DbSet<RoleModel> RoleContext { get; set; }
        public DbSet<UserModel> UserContext { get; set; }
        public DbSet<CodeModel> CodeContext { get; set; }
        public DbSet<CodegroupModel> CodegroupContext { get; set; }
        public DbSet<ClassModel> ClassContext { get; set; }
        public DbSet<CurriculumModel> CurriculumContext { get; set; }
        public DbSet<ClassNoticeModel> ClassNoticeContext { get; set; }
    }
}
