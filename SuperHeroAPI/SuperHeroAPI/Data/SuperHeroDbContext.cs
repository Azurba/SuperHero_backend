using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace SuperHeroAPI.Data
{
    public class SuperHeroDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext>options) : base(options)
        {

        }

        public Microsoft.EntityFrameworkCore.DbSet<SuperHero> SuperHeroes { get; set; }



    }
}
