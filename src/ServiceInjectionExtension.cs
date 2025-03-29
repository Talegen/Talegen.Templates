namespace Talegen.Templates
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The class contains the service extension method for IoC configuration.
    /// </summary>
    public static class ServiceInjectionExtension
    {
        public static IServiceCollection AddFileTemplateProvider(this IServiceCollection services, Action<FileTemplateProviderOptions> config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddOptions();
            services.Configure(config);
            services.AddSingleton<ITemplateProvider, FileTemplateProvider>();
            return services;
        }
    }
}
