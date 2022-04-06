using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Infrastructure.DTO;

public class BakeryDTO
{
    [NotNull]
    public string BakeryCode { get; }
    
    [NotNull]
    public string TownName { get; set; }
    
    [NotNull]
    public string StreetName { get; set; }
    
    [NotNull]
    public int StreetNumber { get; set; }
    
    [NotNull]
    public string PostalCode { get; set; }
}