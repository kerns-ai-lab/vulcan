using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vulcan.Models
{
    public class VulcanDatabaseSettings : IVulcanDatabaseSettings
    {
        public string PlayersCollectionName { get; set; }
        public string GamesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IVulcanDatabaseSettings
    {
        string PlayersCollectionName { get; set; }
        string GamesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
