using Microsoft.EntityFrameworkCore;
using FamilyWallet.Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FamilyWallet.Infraestructure.Data
{
    public class PostgreSQLContext : DbContext
    {
        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Family> Family { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Tope> Topes { get; set; }
    }
}
