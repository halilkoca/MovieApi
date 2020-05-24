using App.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data
{
    public class BaseDbContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public BaseDbContext(DbContextOptions<BaseDbContext> options, IConfiguration configuration)
              : base(options)
        {
            this.configuration = configuration;
        }

        protected BaseDbContext(DbContextOptions options, IConfiguration configuration)
              : base(options)
        {
            this.configuration = configuration;
        }

        public BaseDbContext(DbContextOptions<InMemoryContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseInMemoryDatabase(configuration.GetConnectionString("OASPgContext")));
            }
        }

        public DbSet<User> User { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Actor> Actor { get; set; }

        public void asd()
        {
            
        }

    }
}
