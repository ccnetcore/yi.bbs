using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace CC.Yi.Model
{
    public partial class DataContext :DbContext
    {
        public DbSet<plate> plate { get; set; }
        public DbSet<discuss> discuss { get; set; }
        public DbSet<comment> comment { get; set; }
        public DbSet<user> user { get; set; }
        public DbSet<role> role { get; set; }
        public DbSet<action> action { get; set; }
        public DbSet<level> level { get; set; }
        public DbSet<user_extra> user_extra { get; set; }
        public DbSet<label> label { get; set; }
        public DbSet<collection> collection { get; set; }
        public DbSet<banner> banner { get; set; }
        public DbSet<agree> agree { get; set; }
        public DbSet<version> version { get; set; }
        public DbSet<shop> shop { get; set; }
        public DbSet<warehouse> warehouse { get; set; }
        public DbSet<prop> prop { get; set; }
        public DbSet<friend> friend { get; set; }
        public DbSet<article> article { get; set; }
    }
}