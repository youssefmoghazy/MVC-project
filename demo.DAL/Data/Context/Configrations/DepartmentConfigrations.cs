using Microsoft.EntityFrameworkCore;

namespace demo.DAL.Data.Context.Configrations;

internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
{
    void IEntityTypeConfiguration<Department>.Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(d => d.Id)
               .UseIdentityColumn(10, 10);

        builder.Property(d => d.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(d => d.CreatedOn)
        .HasDefaultValueSql("GETUTCDATE()");
    }
}
