using System;
using Microsoft.EntityFrameworkCore;


namespace ETLAPI.Model{
    public class ETLDbContext : DbContext
    {

        public ETLDbContext() { }

        public ETLDbContext(DbContextOptions<ETLDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<LineItem_01> LineItems_01 { get; set; }
        public DbSet<LineItem_02> LineItems_02 { get; set; }
        public DbSet<LineItem_03> LineItems_03 { get; set; }
        public DbSet<LineItem_04> LineItems_04 { get; set; }
        public DbSet<LineItem_05> LineItems_05 { get; set; }
        public DbSet<LineItem_06> LineItems_06 { get; set; }
        public DbSet<LineItem_07> LineItems_07 { get; set; }
        public DbSet<LineItem_08> LineItems_08 { get; set; }
        public DbSet<LineItem_09> LineItems_09 { get; set; }
        public DbSet<LineItem_10> LineItems_10 { get; set; }
        public DbSet<LineItem_11> LineItems_11 { get; set; }
        public DbSet<LineItem_12> LineItems_12 { get; set; }
        public DbSet<Production> Productions { get; set; }
    }
}