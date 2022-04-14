using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.WebApp.Models;

public class ProductVM
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public string Name { get; set; }
    
    [NotNull]
    public double Price { get; set; }
}