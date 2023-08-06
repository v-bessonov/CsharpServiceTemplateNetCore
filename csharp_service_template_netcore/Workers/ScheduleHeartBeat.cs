namespace CsharpServiceTemplateNetCore.Workers;

public class ScheduleHeartBeat : Worker
{
    private readonly ILogger<ScheduleHeartBeat> _logger;

    public ScheduleHeartBeat(ILogger<ScheduleHeartBeat> logger, IConfiguration config): base(config)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(GetDelay(), stoppingToken); 
            _logger.LogInformation("ScheduleHeartBeat running at: {time}", DateTimeOffset.Now);
        }
    }
}