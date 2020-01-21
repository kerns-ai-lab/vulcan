using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vulcan.Models
{
    public class Match
    {
        public String GameId { get; set; }
        public List<APITeam> Teams { get; set; }
    }
}
