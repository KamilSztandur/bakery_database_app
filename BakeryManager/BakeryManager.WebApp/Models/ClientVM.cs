using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.WebApp.Models;

public class ClientVM
{
    [NotNull]
    public int Id { get; }
    
    [NotNull]
    public string Name { get; set; }
    
    [NotNull]
    public string Surname { get; set; }
}