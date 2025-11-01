using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    { 
        services.AddScoped<IReaderRepository, ReaderRepository>();
        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        
        return services;
    }
}