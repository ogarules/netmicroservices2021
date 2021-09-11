using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Prometheus;
using Steeltoe.Common.Http.Discovery;

namespace Organization
{
    public static class Extensions
    {
        public static System.Uri ToServiceName(this string service)
        {
            return new Uri($"http://{service}/");
        }

        public static IHttpClientBuilder AddMicroserviceOptions(this IHttpClientBuilder builder)
        { 
            return builder.AddServiceDiscovery()
            .AddHeaderPropagation()
            .AddRoundRobinLoadBalancer()
            .UseHttpClientMetrics()
            .AddTransientHttpErrorPolicy(r => r.RetryAsync(3))
            .AddTransientHttpErrorPolicy(r => r.CircuitBreakerAsync(4, TimeSpan.FromSeconds(15)));
        }
    }
}