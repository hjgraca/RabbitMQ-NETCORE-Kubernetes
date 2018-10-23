using System;
using MassTransit.Extensions.Hosting;
using MassTransit.Extensions.Hosting.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GreenPipes;

namespace Receive
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // use the new generic host from ASP.NET Core
            // see for more info: https://github.com/aspnet/Hosting/issues/1163
            new HostBuilder()
                .ConfigureAppConfiguration((context, builder) => ConfigureAppConfiguration(context, builder, args))
                .ConfigureHostConfiguration(config => config.AddEnvironmentVariables())
                .ConfigureServices(ConfigureServices)
                .Build()
                .Run();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder configurationBuilder, string[] args)
        {
            var environmentName = context.HostingEnvironment.EnvironmentName;

            configurationBuilder.AddJsonFile("appsettings.json");
            configurationBuilder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);

            configurationBuilder.AddCommandLine(args);
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            // optionally use configuration for any settings
            var configuration = context.Configuration;
            var rabbitMqOptions = configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();

            // the following adds IBusManager which is also an IHostedService that is started/stopped by HostBuilder
            services.AddMassTransit(busBuilder =>
            {
                busBuilder.UseRabbitMq(rabbitMqOptions, hostBuilder =>
                {
                    // use scopes for all downstream filters and consumers
                    // i.e. per-request lifetime
                    hostBuilder.UseServiceScope();

                    // example adding a receive endpoint to the bus
                    hostBuilder.AddReceiveEndpoint("example-queue-3", endpointBuilder =>
                        {
                            // example adding a consumer to the receive endpoint
                            endpointBuilder.AddConsumer<ExampleConsumer>(configureConsumer =>
                                {
                                    // example adding an optional configurator to the consumer
                                    // using IConsumerConfigurator<TConsumer>
                                    configureConsumer.UseRetry(r => r.Immediate(3));
                                });
                        });
                });
            });
        }
    }
}