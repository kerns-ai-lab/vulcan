﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Vulcan.Models;

namespace Vulcan.Services
{
    public class GameService
    {
        private readonly IMongoCollection<Game> _games;

        public GameService(IVulcanDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<Game>(settings.GamesCollectionName);
        }

        public List<Game> Get() => _games.Find(game => true).ToList();

        public Game Get(string id) => _games.Find<Game>(game => game.Id == id).FirstOrDefault();

        public Game Create(Game game)
        {
            _games.InsertOne(game);
            return game;
        }

        public void Update(string id, Game gameIn) => _games.ReplaceOne(game => game.Id == id, gameIn);

        public void Remove(Game gameIn) => _games.DeleteOne(game => game.Id == gameIn.Id);

        public void Remove(string id) => _games.DeleteOne(game => game.Id == id);
    }
}
