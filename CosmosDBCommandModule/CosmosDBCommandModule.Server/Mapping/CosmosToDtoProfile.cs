using AutoMapper;
using CosmosDBCommandModule.Shared.Models;
using Microsoft.Azure.Documents;

namespace CosmosDBCommandModule.Server.Mapping
{
    public class CosmosToDtoProfile : Profile
    {
        public CosmosToDtoProfile()
        {
            CreateMap<DocumentCollection, CosmosCollection>();
            CreateMap<Database, CosmosDatabase>();
            CreateMap<OfferV2, CosmosOffer>();
        }
    }
}