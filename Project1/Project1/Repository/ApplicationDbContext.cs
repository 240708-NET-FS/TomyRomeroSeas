using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ReviewShelf.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

    public ApplicationDbContext(){}


    public DbSet<Login> Logins {get;set;}
    public DbSet<User> Users {get;set;}

    public DbSet<BookReview> BookReviews {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();
            
            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configuring the User entity
    modelBuilder.Entity<User>(entity =>
    {
        // Setting UserId as the primary key
        entity.HasKey(u => u.UserId);
        entity.Property(u => u.UserId).ValueGeneratedOnAdd();
        entity.Property(u => u.FirstName).IsRequired();
        entity.Property(u => u.LastName).IsRequired();
        entity.Property(u => u.UserName).IsRequired();

        // Configuring one-to-one relationship between User and Login
        // Each User must have one Login, and each Login must have one User
        entity.HasOne(u => u.Login)
              .WithOne(l => l.User)
              .HasForeignKey<Login>(l => l.UserId);

        // Configuring one-to-many relationship between User and BookReview
        // Each User can have multiple BookReviews
        entity.HasMany(u => u.BookReviews)
              .WithOne(br => br.User)
              .HasForeignKey(br => br.UserId);
    });

    // Configuring the Login entity
    modelBuilder.Entity<Login>(entity =>
    {
        // Setting LoginId as the primary key
        entity.HasKey(l => l.LoginId);
        entity.Property(l => l.LoginId).ValueGeneratedOnAdd();
        entity.Property(l => l.Username).IsRequired();
        entity.Property(l => l.Password).IsRequired();
        entity.Property(l => l.UserId).IsRequired();
    });

    // Configuring the BookReview entity
    modelBuilder.Entity<BookReview>(entity =>
    {
        // Setting BookReviewId as the primary key
        entity.HasKey(br => br.BookReviewId);
        entity.Property(br => br.BookReviewId).ValueGeneratedOnAdd();
        entity.Property(br => br.BookTitle).IsRequired();
        entity.Property(br => br.Review).IsRequired();
        entity.Property(br => br.UserId).IsRequired();

        // Configuring the many-to-one relationship between BookReview and User
        //BookReview can refernce its author
        entity.HasOne(br => br.User)
              .WithMany(u => u.BookReviews)
              .HasForeignKey(br => br.UserId);
    });

    // Calling the base OnModelCreating method to ensure any additional configuration is applied
    base.OnModelCreating(modelBuilder);
}

}