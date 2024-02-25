using Microsoft.Extensions.DependencyInjection;

namespace Yak.Core.Abstractions;

public static class ExtensionsModule {
    /// TODO: Investigate the way to remove it in the future.
    public static void AddAbstractionsModule(this IServiceCollection services)
    {
        services.AddTransient<IDomainEventPublisher, DomainEventPublisher>();
    }
}