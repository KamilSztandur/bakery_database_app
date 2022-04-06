using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;

namespace BakeryManager.Core.Domain;

public class Receipt
{
    [NotNull]
    public int Id { get; set; }
    
    [NotNull]
    public int ClientId { get; set; }
    
    [NotNull]
    public int ProductId { get; set; }
    
    [NotNull]
    public String BakeryCode { get; set; }
    
    [NotNull]
    public SqlMoney TotalPrice { get; set; }
    
    [NotNull]
    public DateTime Date { get; set; }
}