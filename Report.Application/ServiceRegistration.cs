using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Report.Application.Assemblers;
using Report.Application.Behaviors;
using Report.Application.Interfaces;
using Report.Application.Middlewares;

namespace Report.Application;

public static class ServiceRegistration
{
    public static void Inject(IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient<ExceptionHandlingMiddleware>();
        services.AddTransient<IReportRequestAssembler, ReportRequestAssembler>();
    }
}