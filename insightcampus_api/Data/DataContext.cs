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
            modelBuilder.Entity<CodeModel>().HasKey(role => new
            {
                role.code_id,
                role.codegroup_id
            });

            modelBuilder.Entity<RoleUserModel>().HasKey(role => new
            {
                role.user_seq,
                role.role_nm
            });
        }
        

        public DbSet<CategoryModel> CategoryContext { get; set; }
        public DbSet<RoleModel> RoleContext { get; set; }
        public DbSet<UserModel> UserContext { get; set; }
        public DbSet<CodeModel> CodeContext { get; set; }
        public DbSet<RoleUserModel> RoleUserContext { get; set; }
        public DbSet<CodegroupModel> CodegroupContext { get; set; }
        public DbSet<ClassModel> ClassContext { get; set; }
        public DbSet<CurriculumModel> CurriculumContext { get; set; }
        public DbSet<CurriculumgroupModel> CurriculumgroupContext { get; set; }
        public DbSet<ClassReviewModel> ClassReviewContext { get; set; }
        public DbSet<ClassNoticeModel> ClassNoticeContext { get; set; }
        public DbSet<IncamAddfareModel> IncamAddfareContext { get; set; }
        public DbSet<IncamContractModel> IncamContractContext { get; set; }
        public DbSet<ClassQnaModel> ClassQnaContext { get; set; }
        public DbSet<OrderModel> OrderContext { get; set; }
        public DbSet<OrderItemModel> OrderItemContext { get; set; }
        public DbSet<TeacherModel> TeacherContext { get; set; }
        public DbSet<EmailLogModel> EmailLogContext { get; set; }
        public DbSet<CouponModel> CouponContext { get; set; }
        public DbSet<FaqModel> FaqContext { get; set; }
        public DbSet<TeacherLogModel> TeacherLogContext { get; set; }
        public DbSet<IncamAddfareLogModel> IncamAddfareLogContext { get; set; }
        public DbSet<IncamContractLogModel> IncamContractLogContext { get; set; }
        public DbSet<EmployProofModel> EmployProofContext { get; set; }
        public DbSet<CommunityModel> CommunityContext { get; set; }
        public DbSet<FinWorkModel> FinWorkContext { get; set; }
        public DbSet<FinWorkDetailModel> FinWorkDetailContext { get; set; }
        public DbQuery<WPBoardNoticeDto> WPBoardNoticeModel { get; set; }
    }
}
