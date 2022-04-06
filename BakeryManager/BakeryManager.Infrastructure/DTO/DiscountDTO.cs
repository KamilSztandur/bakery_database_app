using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Infrastructure.DTO;

public class DiscountDTO
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public double MoneyThreshold { get; set; }
    
    [NotNull]
    public double ValueInPercents { get; set; }
    
    [NotNull]
    public bool IsActive { get; set; }
    
    public string? Description { get; set; }
}