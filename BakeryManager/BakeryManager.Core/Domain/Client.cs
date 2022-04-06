using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Client
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public String name { get; set; }
    
    [NotNull]
    public String Surname { get; set; }
}