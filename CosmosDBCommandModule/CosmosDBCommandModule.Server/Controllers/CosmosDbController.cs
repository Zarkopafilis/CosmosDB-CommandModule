﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cosmonaut;
using CosmosDBCommandModule.Shared.Models;

namespace CosmosDBCommandModule.Server.Controllers
{
    [Route("api")]
    public class CosmosDbController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICosmonautClient _cosmonautClient;

        public CosmosDbController(IMapper mapper, ICosmonautClient cosmonautClient)
        {
            _mapper = mapper;
            _cosmonautClient = cosmonautClient;
        }

        [HttpGet("databases")]
        public async Task<IEnumerable<CosmosDatabase>> GetDatabases([FromQuery] bool withCollections = false, [FromQuery] bool withOffers = false)
        {
            var databases = await _cosmonautClient.QueryDatabasesAsync();

            var cosmosDatabases = _mapper.Map<List<CosmosDatabase>>(databases);

            if (withCollections)
            {
                foreach (var database in databases)
                {
                    var collections = await _cosmonautClient.QueryCollectionsAsync(database.Id);
                    var cosmosCollections = _mapper.Map<IEnumerable<CosmosCollection>>(collections).ToList();
                    cosmosDatabases.Single(x => x.Id == database.Id).Collections = cosmosCollections;

                    if (withOffers)
                    {
                        foreach (var collection in cosmosCollections)
                        {
                            var offer = await _cosmonautClient.GetOfferV2ForCollectionAsync(database.Id, collection.Id);

                            collection.Offer = new CosmosOffer
                                {Id = offer.Id, Throughput = offer.Content.OfferThroughput};
                        }
                    }
                }
            }

            return cosmosDatabases;
        }
    }
}
