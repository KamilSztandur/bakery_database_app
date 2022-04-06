using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Product
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public string Name { get; set; }
    
    [NotNull]
    public double Price { get; set; }
}