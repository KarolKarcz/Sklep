using API.Entities;
using API.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<EmailConfirmation> EmailConfirmation { get; set; }
        public DbSet<PasswordReset> PasswordReset { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
