using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CommentManagementService.Domain.Comments;
using CommentManagementService.Domain.Comments.ValueObjects;

namespace CommentManagementService.Persistence.Comments.Mappers;

public class CommentorConfiguration : IEntityTypeConfiguration<Commentor>
{
    public void Configure(EntityTypeBuilder<Commentor> builder)
    {
        builder.ToTable(nameof(Commentor));

        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                v => v.Value,
                v => CommentorId.Create(v).Value)
            .ValueGeneratedNever();

        builder.OwnsOne(p => p.UserName, c =>
        {
            c.Property(x => x.Value).HasColumnName("UserName").HasColumnType("NVARCHAR(256)").IsRequired();
        });
    }
}
