using CleanArchitectureTemplate.AI.Services;
using CleanArchitectureTemplate.Application.Abstractions.AI;
using Codeblaze.SemanticKernel.Connectors.Ollama;
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
        services.AddKernel()
            .AddOllamaChatCompletion(
                modelId: configuration["AI:Ollama:Model"]!,
                baseUrl: configuration["AI:Ollama:ServerUrl"]!);
        //.AddOpenAIChatCompletion(
        //    modelId: configuration["AI:OpenAI:Model"]!,
        //    openAIClient: new OpenAIClient
        //    (
        //        credential: new ApiKeyCredential(configuration["AI:OpenAI:ApiKey"]!),
        //        options: new OpenAIClientOptions()
        //        {
        //            Endpoint = new Uri(configuration["AI:OpenAI:ServerUrl"]!)
        //        }
        //    ));

        services.AddScoped<IAIService, SemanticKernelAIService>();
        services.AddSingleton<IChatHistoryService, SemanticKernelChatHistoryService>();

        return services;
    }
}