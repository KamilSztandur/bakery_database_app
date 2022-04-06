namespace BakeryManager.Infrastructure.Commands;

public class CreateDiscount
{
    public double MoneyThreshold { get; set; }
    
    public double ValueInPercents { get; set; }
    
    public bool IsActive { get; set; }
    
    public string? Description { get; set; }
}