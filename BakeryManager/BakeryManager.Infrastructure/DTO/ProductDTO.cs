using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Infrastructure.DTO;

public class ProductDTO
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public string Name { get; set; }
    
    [NotNull]
    public SqlMoney Price { get; set; }
}