﻿using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.WebApp.Models;

public class DiscountVM
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public double MoneyThreshold { get; set; }
    
    [NotNull]
    public double ValueInPercents { get; set; }
    
    [NotNull]
    public bool IsActive { get; set; }
    
    public String? Description { get; set; }
}