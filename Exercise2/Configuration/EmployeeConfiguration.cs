using Exercise2.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exercise2.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true).IsUnicode(true).HasMaxLength(255);
            builder.Property(x => x.PositionID).IsRequired(true);
            builder.HasOne(x => x.Position).WithMany(x => x.Employees).HasForeignKey(x => x.PositionID);
        }
    }
}
