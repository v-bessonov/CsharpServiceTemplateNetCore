using Prometheus;

namespace CsharpServiceTemplateNetCore.DependencyInjection;

public static class MetricsDi
{
    public static WebApplication SetMetrics
    (
        this WebApplication app
    )
    {
        app.UseMetricServer();
        app.UseHttpMetrics();

        return app;
    }
}