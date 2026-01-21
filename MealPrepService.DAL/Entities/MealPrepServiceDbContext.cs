using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepService.DAL.Entities
{
    public class MealPrepDbContext : DbContext
    {
        public MealPrepDbContext(DbContextOptions<MealPrepDbContext> options) : base(options) { }
    }
}
