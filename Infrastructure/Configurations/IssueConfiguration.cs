using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class IssueConfiguration: IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasOne(i => i.Book).WithMany().HasForeignKey(i => i.LibraryCode);
        builder.HasOne(i => i.Reader).WithMany().HasForeignKey(i => i.ReaderCardNumber);
    }
}