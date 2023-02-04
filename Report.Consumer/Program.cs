using Microsoft.Extensions.Hosting;
using MassTransit;
using Report.Consumer.Consumers;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    Report.Infrastructure.ServiceRegistration.Inject(services);
    Report.Persistence.ServiceRegistration.Inject(services);
    Report.Application.ServiceRegistration.Inject(services);

    services.AddMassTransit(busConfigurator =>
    {
        busConfigurator.AddConsumer<ReportRequestedMessageConsumer>();
        busConfigurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            });
            cfg.ReceiveEndpoint("reportRequestQueue", ep =>
            {
                ep.PrefetchCount = 16;
                ep.UseMessageRetry(r => r.Interval(2, 100));
                ep.ConfigureConsumer<ReportRequestedMessageConsumer>(provider);
            });
        }));
    });
});

var app = builder.Build();

app.Run();