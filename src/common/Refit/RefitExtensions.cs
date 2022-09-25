using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Timeout;
using Refit;

namespace common.Refit;

public static class RefitExtensions
{
    public static IServiceCollection AddRefitWithRetryPolicy<T>(this IServiceCollection services, string baseAddress)
        where T : class
    {
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromMilliseconds(1), 3));

        services.AddRefitClient<T>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(retryPolicy));

        return services;
    }
}