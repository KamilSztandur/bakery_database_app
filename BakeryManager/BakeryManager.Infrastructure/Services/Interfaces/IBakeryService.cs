using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;

namespace BakeryManager.Infrastructure.Services.Interfaces;

public interface IBakeryService
{
    Task<int> UpdateBakery(String bakeryCode, CreateBakery bakeryBody);
    Task<int> DeleteBakery(String bakeryCode);
    Task<int> AddBakery(CreateBakery bakeryBody);
    Task<BakeryDTO?> GetBakery(String bakeryCode);
    Task<IEnumerable<BakeryDTO>> BrowseAll();
}