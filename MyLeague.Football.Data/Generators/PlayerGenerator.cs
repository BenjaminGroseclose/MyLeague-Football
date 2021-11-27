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
        public static PlayerGeneratorValues CreateDefaultPlayer()
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
                List<PlayerAttributes> attributes = new List<PlayerAttributes>();

                Random rand = new Random();

                var firstNamesRaw = File.ReadAllText(Path.Combine($"{basePath}/Generators/Json", "firstNames.json"));
                var firstNames = JsonSerializer.Deserialize<List<string>>(firstNamesRaw);

                var lastNamesRaw = File.ReadAllText(Path.Combine($"{basePath}/Generators/Json", "lastNames.json"));
                var lastNames = JsonSerializer.Deserialize<List<string>>(lastNamesRaw);

                var collegesRaw = File.ReadAllText(Path.Combine($"{basePath}/Generators/Json", "colleges.json"));
                var colleges = JsonSerializer.Deserialize<List<string>>(collegesRaw);

                int rowId = 1;

                foreach (var initialPlayer in initialPlayers)
                {
                    string firstName = firstNames.ElementAt(rand.Next(firstNames.Count));
                    string lastName = lastNames.ElementAt(rand.Next(lastNames.Count));
                    string college = colleges.ElementAt(rand.Next(colleges.Count));

                    PlayerAttributes playerAttributes = mapper.Map<PlayerAttributes>(initialPlayer);
                    playerAttributes.Id = rowId;

                    attributes.Add(playerAttributes);
                    players.Add(new Player(rowId, firstName, lastName, college, initialPlayer, playerAttributes));

                    rowId++;
                }

                return new PlayerGeneratorValues(players, attributes);
            }
        }
    }
}
