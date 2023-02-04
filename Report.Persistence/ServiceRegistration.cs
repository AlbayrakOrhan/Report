using Report.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Report.Persistence.Context;
using Report.Persistence.Repositories;
using Report.Persistence.UnitOfWorks;

namespace Report.Persistence;

public static class ServiceRegistration
{
    public static void Inject(IServiceCollection services)
    {
        services.AddDbContext<ReportDbContext>(options => options.UseNpgsql("Host=localhost;Port=54320;Username=postgres;Password=testdb;Database=Report", x => x.MigrationsAssembly("Report.Persistence")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReportRequestRepository, ReportRequestRepository>();
    }
}