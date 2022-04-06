namespace BakeryManager.Infrastructure.Commands;

public class CreateBakery
{
    public string? BakeryCode { get; }
    
    public string? TownName { get; set; }
    
    public string? StreetName { get; set; }
    
    public int StreetNumber { get; set; }
    
    public string? PostalCode { get; set; }
}