using CleanArchitectureTemplate.AI.Plugins;
using CleanArchitectureTemplate.AI.Services;
using CleanArchitectureTemplate.Application.Abstractions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using OpenAI;
using System.ClientModel;

namespace CleanArchitectureTemplate.AI;

/// <summary>
/// Represents the service registration for the artificial intelligence (AI) layer.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Adds the AI services to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <param name="configuration">The configuration to use for the services.</param>
    public static IServiceCollection AddAIServices(this IServiceCollection services, IConfiguration configuration)
    {
        var kernel = services.AddKernel()
        .AddOpenAIChatCompletion(
            modelId: configuration["AI:OpenAI:Model"]!,
            openAIClient: new OpenAIClient
            (
                credential: new ApiKeyCredential(configuration["AI:OpenAI:ApiKey"]!),
                options: new OpenAIClientOptions()
                {
                    Endpoint = new Uri(configuration["AI:OpenAI:ServerUrl"]!)
                }
            ));

        kernel.Plugins.AddFromType<TextPlugin>();
        kernel.Plugins.AddFromType<DateTimePlugin>();

        services.AddScoped<IAIService, SemanticKernelAIService>();
        services.AddSingleton<IChatHistoryService, SemanticKernelChatHistoryService>();

        return services;
    }
}