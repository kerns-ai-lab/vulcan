using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vulcan.Models
{
    public class APITeam
    {
        public string Team { get; set; }
        public List<string> Players { get; set; }
        public int MatchRank { get; set; }
    }
}
