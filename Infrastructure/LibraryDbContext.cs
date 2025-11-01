using System.Reflection;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class LibraryDbContext:DbContext
{
    public DbSet<Reader> Readers  { get; set; }
    
    public DbSet<Book> Books   { get; set; }
    
    public DbSet<Issue> Issues { get; set; }
    

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(LibraryDbContext)));
    }
}