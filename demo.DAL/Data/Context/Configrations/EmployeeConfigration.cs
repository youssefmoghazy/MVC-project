
using demo.DAL.Models.Common;

namespace demo.DAL.Data.Context.Configrations
{
    internal class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
            builder.Property(e => e.Address)
                .IsRequired()
                .HasColumnType("varchar(50)");
            builder.Property(e => e.gender)
                .HasConversion(
                Gender => Gender.ToString(), 
                Gender => Enum.Parse<Gender>(Gender)
                );
            builder.Property(e => e.EmployeeType)
                .HasConversion(
                type => type.ToString(),
                type => Enum.Parse<EmployeeType>(type)
                );
            builder.HasOne(e => e.Department)
                .WithMany(d => d.employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
