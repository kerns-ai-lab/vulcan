using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vulcan.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public double InitialMean { get; set; }
        public double InitialStd { get; set; }
        public double Beta { get; set; }
        public double DynamicsFactor { get; set; }
        public double DrawProbability { get; set; }
    }
}
