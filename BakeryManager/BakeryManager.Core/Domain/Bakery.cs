using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Bakery
{
    [NotNull]
    public String BakeryCode { get; }
    
    public String TownName { get; set; }
    
    public String StreetName { get; set; }
    
    public int StreetNumber { get; set; }
    
    public String PostalCode { get; set; }
}