using Microsoft.EntityFrameworkCore;
using SudokuRecord.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuRecord.EntityFrameworkCore
{
    internal class MatDbContext : DbContext
    {
        public virtual DbSet<Mat> Mats { get; set; }

        public MatDbContext(DbContextOptions<MatDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<Mat>().HasKey(e => e.Id);
    }
}
