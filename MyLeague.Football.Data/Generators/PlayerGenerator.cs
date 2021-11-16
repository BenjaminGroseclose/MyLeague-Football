using AutoMapper;
using CsvHelper;
using MyLeague.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyLeague.Football.Data.Generators
{
    public static class PlayerGenerator
    {
        public static async Task CreateDefaultPlayer(MyLeagueFootballContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InitialPlayerModel, PlayerAttributes>();
            });

            var mapper = new Mapper(config);

            var basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            using (var reader = new StreamReader($"{basePath}/Generators/CSV/playerData.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var initialPlayers = csv.GetRecords<InitialPlayerModel>();

                List<Player> players = new List<Player>();

                Random rand = new Random();

                var firstNamesRaw = File.ReadAllText(Path.Combine($"{basePath}/Generators/Json", "firstNames.json"));
                var firstNames = JsonSerializer.Deserialize<List<string>>(firstNamesRaw);

                var lastNamesRaw = File.ReadAllText(Path.Combine($"{basePath}/Generators/Json", "lastNames.json"));
                var lastNames = JsonSerializer.Deserialize<List<string>>(lastNamesRaw);

                var collegesRaw = File.ReadAllText(Path.Combine($"{basePath}/Generators/Json", "colleges.json"));
                var colleges = JsonSerializer.Deserialize<List<string>>(collegesRaw);

                foreach (var player in initialPlayers)
                {
                    string firstName = firstNames.ElementAt(rand.Next(firstNames.Count));
                    string lastName = lastNames.ElementAt(rand.Next(lastNames.Count));
                    string college = colleges.ElementAt(rand.Next(colleges.Count));
                    players.Add(new Player(firstName, lastName, college, player, mapper));
                }

                context.Players.AddRange(players);
                await context.SaveChangesAsync();
            }
        }
    }
}
