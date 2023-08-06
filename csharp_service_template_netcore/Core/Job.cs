namespace CsharpServiceTemplateNetCore.Core;

public class Job
{
    public string Type { get; set; }
        
    public string Description { get; set; }
    
    public int? Delay { get; set; }

    public List<Trigger> Triggers { get; set; }
}