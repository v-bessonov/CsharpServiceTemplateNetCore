﻿namespace CsharpServiceTemplateNetCore.Models;

public class Error
{
    public int Code { get; set; }
    
    public string Message { get; set; }
    
    public string? StackTrace { get; set; }
}