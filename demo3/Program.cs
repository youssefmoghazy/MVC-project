using demo.BLL.Services;
using demo.BLL.Services.AttachmentSevices;
using demo.DAL.Data.Context;
using demo.DAL.Repositories;
using Microsoft.EntityFrameworkCore;


namespace demo3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            builder.Services.AddScoped<Func<IDepartmentRepository>>
                (provider => provider.GetRequiredService<IDepartmentRepository>);
            builder.Services.AddScoped<Func<IEmployeeRepository>>
               (provider => provider.GetRequiredService<IEmployeeRepository>);

            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();

            builder.Services.AddAutoMapper(typeof(demo.BLL.AssemblyReference).Assembly);
            //builder.Services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));

            builder.Services.AddDbContext<ApplicationDBContext>( options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer("Server=.;Database=DemoMVC03;Trusted_Connection=True;TrustServerCertificate=True").UseLazyLoadingProxies();

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
