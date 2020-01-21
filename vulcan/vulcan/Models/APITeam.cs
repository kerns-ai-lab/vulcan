using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vulcan.Models
{
    public class APITeam
    {
        public string Id { get; set; }
        public List<APIPlayer> Players { get; set; }
        public int MatchRank { get; set; }
    }
}
