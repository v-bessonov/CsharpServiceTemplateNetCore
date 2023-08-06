namespace CsharpServiceTemplateNetCore.Workers;

public class HeartBeat : Worker
{
    private readonly ILogger<HeartBeat> _logger;

    public HeartBeat(ILogger<HeartBeat> logger, IConfiguration config) : base (config)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Heartbeat running at: {time}", DateTimeOffset.Now);
            await Task.Delay(GetDelay(), stoppingToken);
        }
    }
}