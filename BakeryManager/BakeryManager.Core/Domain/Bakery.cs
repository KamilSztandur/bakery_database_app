using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Bakery
{
    [NotNull]
    public string BakeryCode { get; set;  }
    
    [NotNull]
    public string TownName { get; set; }
    
    [NotNull]
    public string StreetName { get; set; }
    
    [NotNull]
    public int StreetNumber { get; set; }
    
    [NotNull]
    public string PostalCode { get; set; }
}