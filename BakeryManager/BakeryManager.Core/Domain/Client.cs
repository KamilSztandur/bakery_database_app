﻿using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Client
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public string name { get; set; }
    
    [NotNull]
    public string Surname { get; set; }
}