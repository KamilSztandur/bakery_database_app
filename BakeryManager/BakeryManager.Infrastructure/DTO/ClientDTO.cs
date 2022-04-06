using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Infrastructure.DTO;

public class ClientDTO
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public string Name { get; set; }
    
    [NotNull]
    public string Surname { get; set; }
}