using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.DTO.WalksDto;

namespace NZWalks.API.Mapping;

public class AutoMapperProfiles:Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionsDTO>().ReverseMap();
        CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

        CreateMap<AddWalksRequestDTO, Walk>().ReverseMap();
            
}