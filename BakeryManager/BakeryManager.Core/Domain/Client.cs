using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Client
{
    [NotNull]
    public int Id { get; }
    
    [NotNull]
    public string Name { get; set; }
    
    [NotNull]
    public string Surname { get; set; }
}