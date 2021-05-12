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
    }
}