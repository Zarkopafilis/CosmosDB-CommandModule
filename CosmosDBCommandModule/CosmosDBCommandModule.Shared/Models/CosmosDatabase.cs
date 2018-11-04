using System.Collections.Generic;

namespace CosmosDBCommandModule.Shared.Models
{
    public class CosmosDatabase
    {
        public string Id { get; set; }

        public IEnumerable<CosmosCollection> Collections { get; set; }
    }
}