using BakeryManager.Core.Domain;
using BakeryManager.Core.Repositories;
using BakeryManager.Infrastructure.Commands;
using BakeryManager.Infrastructure.DTO;
using BakeryManager.Infrastructure.Services.Interfaces;

namespace BakeryManager.Infrastructure.Services;

public class BakeryService : IBakeryService
{
    private readonly IBakeryRepository _bakeryRepository;
    
    public BakeryService(IBakeryRepository bakeryRepository)
    {
        _bakeryRepository = bakeryRepository;
    }


    public async Task<int> UpdateBakery(string bakeryCode, CreateBakery bakeryBody)
    {
        if (!IsBodyValid(bakeryBody))
        {
            return -1;
        }
        
        var bakery = ParseCreateBakeryIntoBakery(bakeryBody);
        var result = await _bakeryRepository.UpdateAsync(bakeryCode, bakery);
        
        return result;
    }

    public async Task<int> DeleteBakery(string bakeryCode)
    {
        var result = await _bakeryRepository.DelAsync(bakeryCode);
        
        return result;
    }

    public async Task<int> AddBakery(CreateBakery bakeryBody)
    {
        if (!IsBodyValid(bakeryBody))
        {
            return -1;
        }
        
        var bakery = ParseCreateBakeryIntoBakery(bakeryBody);
        var result = await _bakeryRepository.AddAsync(bakery);

        return result;
    }

    public async Task<BakeryDTO?> GetBakery(string bakeryCode)
    {
        var bakery = await _bakeryRepository.GetAsync(bakeryCode);

        if (bakery == null)
        {
            return null;
        }
        else
        {
            return ParseBakeryIntoBakeryDTO(bakery);
        }
    }

    public async Task<IEnumerable<BakeryDTO>> BrowseAll()
    {
        var bakeries = await _bakeryRepository.BrowseAllAsync();

        var bakeriesDTOs = bakeries.Select(bakery => ParseBakeryIntoBakeryDTO(bakery));

        return bakeriesDTOs;
    }
    
    private static bool IsBodyValid(CreateBakery body) => 
        body.BakeryCode != null && 
        body.StreetName != null &&
        body.TownName != null &&
        body.PostalCode != null;
    
    private BakeryDTO ParseBakeryIntoBakeryDTO(Bakery bakery)
    {
        return new BakeryDTO()
        {
            BakeryCode = bakery.BakeryCode,
            PostalCode = bakery.PostalCode,
            TownName = bakery.TownName,
            StreetName = bakery.StreetName,
            StreetNumber = bakery.StreetNumber
        };
    }

    private Bakery ParseCreateBakeryIntoBakery(CreateBakery bakery)
    {
        return new Bakery()
        {
            BakeryCode = bakery.BakeryCode!,
            PostalCode = bakery.PostalCode!,
            StreetName = bakery.StreetName!,
            StreetNumber = bakery.StreetNumber
        };
    }
}