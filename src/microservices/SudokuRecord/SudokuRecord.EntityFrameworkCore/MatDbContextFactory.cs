using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuRecord.EntityFrameworkCore
{
    internal class MatDbContextFactory : IDesignTimeDbContextFactory<MatDbContext>
    {
        public MatDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MatDbContext> builder = new DbContextOptionsBuilder<MatDbContext>();
            builder.UseSqlServer("Server=sqlserver;Database=Sudoku;User ID=sa;Password=L114514114514;Integrated Security=False;Encrypt=True;Trust Server Certificate=True;Multiple Active Result Sets=True;");
            return new MatDbContext(builder.Options);

        }
    }
}
