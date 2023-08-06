using CsharpServiceTemplateNetCore.Core;
using NCrontab;

namespace CsharpServiceTemplateNetCore.Workers;

public abstract class Worker: BackgroundService
{
    private readonly IConfiguration _configuration;

    protected readonly Job Job;

    protected const int DefaultDelay = 1000;

    protected Worker(IConfiguration config)
    {
        _configuration = config;
        Job = SetJob();
    }
    
    private Job SetJob()
    {
        var jobName = this.GetType().Name;

        var jobsList = new List<Job>();
        _configuration.GetSection("Jobs").Bind(jobsList);

        return jobsList.Find(_ => _.Type == jobName);
    }

    protected int GetDelay()
    {
        var triggers = Job?.Triggers;
        if (triggers?.Any() ?? false)
        {
            var nextRun = GetNextRun(triggers);
            if (nextRun is not null)
            {
                return UntilNextExecution(nextRun.Value);
            }
        }

        return Job?.Delay ?? DefaultDelay;
    }
    
    private static int UntilNextExecution(DateTime nextRun) 
        => Math.Max(0, (int)nextRun.Subtract(DateTime.Now).TotalMilliseconds);


    private static DateTime? GetNextRun(List<Trigger> triggers)
    {
        var nextRuns = new List<DateTime>();
        foreach (var trigger in triggers)
        {
            var crontabSchedule
                = CrontabSchedule.Parse(trigger.Cron, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            nextRuns.Add(crontabSchedule.GetNextOccurrence(DateTime.Now));
        }

        return nextRuns.Any() ? nextRuns.Min() : default;
    }
}