using App.Core.Entities;
using App.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;

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

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
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
        public DbSet<OClaim> OClaim { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<UserClaim> UserClaim { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>(rr =>
            {
                rr.HasKey(e => e.Id);
                rr.HasMany(e => e.Actors);
                rr.HasMany(e => e.Genres);
            });

            modelBuilder.Entity<Actor>(rr =>
            {
                rr.HasKey(e => e.Id);
                rr.HasMany(c => c.Movies);
            });

            modelBuilder.Entity<User>(rr =>
            {
                rr.HasMany(a => a.UserClaims);
            });

            modelBuilder.Entity<Genre>(rr =>
            {
                rr.HasKey(e => e.Id);
            });

            modelBuilder.Entity<OClaim>(rr =>
            {
                rr.HasKey(e => e.Id);
            });
        }

        
    }

    public static class DbContextExtensions
    {
        public static void ResetValueGenerators(this DbContext context)
        {
            var cache = context.GetService<IValueGeneratorCache>();

            foreach (var keyProperty in context.Model.GetEntityTypes()
                .Select(e => e.FindPrimaryKey().Properties[0])
                .Where(p => p.ClrType == typeof(int)
                            && p.ValueGenerated == ValueGenerated.OnAdd))
            {
                var generator = (ResettableValueGenerator)cache.GetOrAdd(
                    keyProperty,
                    keyProperty.DeclaringEntityType,
                    (p, e) => new ResettableValueGenerator());

                generator.Reset();
            }
        }
    }

    public class ResettableValueGenerator : ValueGenerator<int>
    {
        private int _current;

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
            => Interlocked.Increment(ref _current);

        public void Reset() => _current = 0;
    }
}
