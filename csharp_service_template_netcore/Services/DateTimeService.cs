using CsharpServiceTemplateNetCore.Interfaces;

namespace CsharpServiceTemplateNetCore.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}