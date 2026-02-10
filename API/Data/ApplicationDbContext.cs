using API.Models.Static;
using API.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data 
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public DbSet<Ability> Ability { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<PkmnType> PkmnType { get; set; }
        public DbSet<BaseStats> BaseStats { get; set; }
        public DbSet<Move> Move { get; set; }
        public DbSet<MoveEffect> MoveEffect { get; set; }
        public DbSet<DamageClass> DamageClass { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Nature> Nature { get; set; }

        public DbSet<Team> Team { get; set; }
        public DbSet<UserPokemon> UserPokemon { get; set; }

        public DbSet<PokemonPkmnType> PokemonPkmnType { get; set; }
        public DbSet<PokemonAbility> PokemonAbility { get; set; }
        public DbSet<PokemonMove> PokemonMove { get; set; }
        public DbSet<PokemonGender> PokemonGender { get; set; }


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
       
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Register Pokemon-BaseStats one-to-one relationship
            modelBuilder.Entity<Pokemon>()
                .HasOne(e => e.BaseStats)
                .WithOne(e => e.Pokemon)
                .HasForeignKey<BaseStats>(e => e.PokemonId);


            // Register Pokemon-PkmnType many-to-many relationship
            modelBuilder.Entity<Pokemon>()
                .HasMany(e => e.PkmnTypes)
                .WithMany(e => e.Pokemon)
                .UsingEntity<PokemonPkmnType>();


            // Register Pokemon-Ability many-to-many relationship
            modelBuilder.Entity<Pokemon>()
                .HasMany(e => e.Abilities)
                .WithMany(e => e.Pokemon)
                .UsingEntity<PokemonAbility>();


            // Register Pokemon-Gender many-to-many relationship
            modelBuilder.Entity<Pokemon>()
                .HasMany(e => e.Genders)
                .WithMany(e => e.Pokemon)
                .UsingEntity<PokemonGender>();


            // Register Pokemon-Move many-to-many relationship
            modelBuilder.Entity<Pokemon>()
                .HasMany(e => e.Moves)
                .WithMany(e => e.Pokemon)
                .UsingEntity<PokemonMove>();



            // Add Identity Roles
            modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole>()
                {
                    new()
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new()
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    }
                }
            );


            // Add Test Users
            modelBuilder.Entity<AppUser>().HasData(new List<AppUser>()
                {
                    new()
                    {
                        Id = "test1",
                        UserName = "TestUser1"
                    },
                    new()
                    {
                        Id = "test2",
                        UserName = "TestUser2"
                    }
                }
            );
        }
    }
}