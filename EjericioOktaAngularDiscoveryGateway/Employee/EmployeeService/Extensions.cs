using System;
using Microsoft.Extensions.DependencyInjection;

using Steeltoe.Discovery.Client;
using Steeltoe.Common.Http.Discovery;
using Prometheus;
using Polly;

namespace EmployeeService
{
    public static class Extensions
    {
        public static Uri ToServiceName(this string service)
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