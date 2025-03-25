using CleanArchitectureTemplate.AI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace CleanArchitectureTemplate.AI.Cache
{
    public static class KernelFunctionCache
    {
        private static Lazy<KernelFunction>? _detectLanguageFn;
        private static Lazy<KernelFunction>? _translateFn;
        private static Lazy<KernelFunction>? _summarizeFn;
        private static bool _initialized = false;
        private static readonly Lock _lock = new();

        public static void Initialize(Kernel kernel, IPromptTemplateFactory templateFactory, string promptDir, ILogger<SemanticKernelAIService> logger)
        {
            if (kernel == null)
                throw new ArgumentNullException(nameof(kernel));
            if (templateFactory == null)
                throw new ArgumentNullException(nameof(templateFactory));
            if (string.IsNullOrWhiteSpace(promptDir))
                throw new ArgumentException("Prompt directory must not be null or empty", nameof(promptDir));

            if (!_initialized)
            {
                lock (_lock)
                {
                    if (!_initialized)
                    {
                        string detectLanguagePath = Path.Combine(promptDir, "Language", "detect_language.yaml");
                        string translatePath = Path.Combine(promptDir, "Language", "translate.yaml");
                        string summarizePath = Path.Combine(promptDir, "Language", "summarize.yaml");

                        if (!File.Exists(detectLanguagePath))
                            throw new FileNotFoundException($"Detect language prompt file not found at {detectLanguagePath}");
                        if (!File.Exists(translatePath))
                            throw new FileNotFoundException($"Translate prompt file not found at {translatePath}");
                        if (!File.Exists(summarizePath))
                            throw new FileNotFoundException($"Summarize prompt file not found at {summarizePath}");

                        _detectLanguageFn = new Lazy<KernelFunction>(() =>
                            kernel.CreateFunctionFromPromptYaml(
                                File.ReadAllText(detectLanguagePath),
                                templateFactory
                            ),
                            LazyThreadSafetyMode.ExecutionAndPublication);

                        _translateFn = new Lazy<KernelFunction>(() =>
                            kernel.CreateFunctionFromPromptYaml(
                                File.ReadAllText(translatePath),
                                templateFactory
                            ),
                            LazyThreadSafetyMode.ExecutionAndPublication);

                        _summarizeFn = new Lazy<KernelFunction>(() =>
                            kernel.CreateFunctionFromPromptYaml(
                                File.ReadAllText(summarizePath),
                                templateFactory
                            ),
                            LazyThreadSafetyMode.ExecutionAndPublication);

                        _initialized = true;

                        logger.LogInformation("Kernel functions initialized with prompt directory: {PromptDir}", promptDir);
                    }
                }
            }
        }

        public static KernelFunction DetectLanguageFn
        {
            get
            {
                if (!_initialized || _detectLanguageFn == null)
                    throw new InvalidOperationException("KernelFunctionCache is not initialized. Call Initialize() first.");
                return _detectLanguageFn.Value;
            }
        }

        public static KernelFunction TranslateFn
        {
            get
            {
                if (!_initialized || _translateFn == null)
                    throw new InvalidOperationException("KernelFunctionCache is not initialized. Call Initialize() first.");
                return _translateFn.Value;
            }
        }

        public static KernelFunction SummarizeFn
        {
            get
            {
                if (!_initialized || _summarizeFn == null)
                    throw new InvalidOperationException("KernelFunctionCache is not initialized. Call Initialize() first.");
                return _summarizeFn.Value;
            }
        }
    }
}