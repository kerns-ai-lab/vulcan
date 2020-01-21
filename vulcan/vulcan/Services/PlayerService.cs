using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;
using Vulcan.Models;

namespace Vulcan.Services
{
    public class PlayerService
    {
        private readonly IMongoCollection<APIPlayer> _players;

        public PlayerService(IVulcanDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _players = database.GetCollection<APIPlayer>(settings.PlayersCollectionName);
        }

        public List<APIPlayer> Get() => _players.Find(player => true).ToList();

        public APIPlayer Get(string id) => _players.Find<APIPlayer>(player => player.Id == id).FirstOrDefault();

        public APIPlayer Create(APIPlayer player)
        {
            _players.InsertOne(player);
            return player;
        }

        public void Update(string id, APIPlayer playerIn) => _players.ReplaceOne(player => player.Id == id, playerIn);

        public void Remove(APIPlayer playerIn) => _players.DeleteOne(player => player.Id == playerIn.Id);

        public void Remove(string id) => _players.DeleteOne(player => player.Id == id);

    }
}
