using Exercise2.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Exercise2.Configuration
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("Position");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property<string>(x => x.Name).IsRequired(true).IsUnicode(true).HasMaxLength(255);

        }
    }
}
