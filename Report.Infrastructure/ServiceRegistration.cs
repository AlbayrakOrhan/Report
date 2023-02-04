using Microsoft.Extensions.DependencyInjection;
using Report.Application.Interfaces;
using Report.Infrastructure.Commons;
using Report.Infrastructure.ContactService;

namespace Report.Infrastructure;

public static class ServiceRegistration
{
    public static void Inject(IServiceCollection services)
    {
        services.AddSingleton<ICommunicatorBase, CommunicatorBase>();
        services.AddHttpClient<IContactServiceCommunicator, ContactServiceCommunicator>(
            client =>
            {
                client.BaseAddress = new Uri("http://localhost:5155");
                client.Timeout = TimeSpan.FromSeconds(19);
            });
    }
}