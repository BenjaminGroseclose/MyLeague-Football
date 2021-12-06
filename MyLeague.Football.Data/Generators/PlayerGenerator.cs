using AutoMapper;
using CsvHelper;
using MyLeague.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MyLeague.Football.Data.Generators
{
    public static class PlayerGenerator
    {
        public static PlayerGeneratorValues CreateDefaultPlayer()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InitialPlayerModel, PlayerAttributes>()
                   .ForMember(dest => dest.DeepBall, act => act.MapFrom(src => src.ThrowAccuracyDeep))
                   .ForMember(dest => dest.ShortAccuracy, act => act.MapFrom(src => src.ThrowAccuracyShort))
                   .ForMember(dest => dest.ThrowOnRun, act => act.MapFrom(src => src.ThrowOnTheRun))
                   .ForMember(dest => dest.PowerMove, act => act.MapFrom(src => src.PowerMoves))
                   .ForMember(dest => dest.FinesseMove, act => act.MapFrom(src => src.FinesseMoves))
                   .ForMember(dest => dest.ImpactBlock, act => act.MapFrom(src => src.ImpactBlocking))
                   .ForMember(dest => dest.RouteRunning, act => act.MapFrom(src => (src.DeepRouteRunning + src.MediumRouteRunning + src.ShortRouteRunning) / 3));
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

                var collegesRaw = File.ReadAllText(Path.Combine($"{basePath}/Generators/Json", "colleges.json"));
                var colleges = JsonSerializer.Deserialize<List<string>>(collegesRaw);

                int rowId = 1;

                foreach (var initialPlayer in initialPlayers)
                {
                    var playerName = GetPlayersName(initialPlayer.PlayerName);
                    string college = colleges.ElementAt(rand.Next(colleges.Count));

                    PlayerAttributes playerAttributes = mapper.Map<PlayerAttributes>(initialPlayer);
                    playerAttributes.Id = rowId;

                    attributes.Add(playerAttributes);
                    players.Add(new Player(rowId, playerName.firstName, playerName.lastName, college, initialPlayer, playerAttributes));

                    rowId++;
                }

                return new PlayerGeneratorValues(players, attributes);
            }
        }

        public static (string firstName, string lastName) GetPlayersName(string intialGame)
        {
            Regex regex = new Regex(@"([A-Z][a-z]*)");

            var matches = regex.Matches(intialGame);

            string lastName = matches[0].Value;
            string firstName = matches[1].Value;

            return (firstName, lastName);
        }
    }
}
