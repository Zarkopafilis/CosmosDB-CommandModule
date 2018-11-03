using Microsoft.AspNetCore.Mvc;
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
        public async Task<IEnumerable<CosmosDatabase>> GetDatabases()
        {
            var databases = await _cosmonautClient.QueryDatabasesAsync();

            return _mapper.Map<IEnumerable<CosmosDatabase>>(databases);
        }
    }
}
