namespace CsharpServiceTemplateNetCore.Models;

public class JwtToken
{
    public string Token { get; set; }
    public DateTime ExpiredOn  { get; set; }
}