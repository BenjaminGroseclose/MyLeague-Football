using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MyLeague.Football.Data.Generators;
using MyLeague.Football.Data.Models;
using System;
using System.Threading.Tasks;

namespace MyLeague.Football.Data
{
    public static class DatabaseInitializer
    {
        /// <summary>
        /// Initalizes the database
        /// </summary>
        /// <param name="context">The context that will be used to write to the DB</param>
        /// <returns>If successfully intialized</returns>
        public static async Task<bool> Init(MyLeagueFootballContext context)
        {
            // Gets the only user
            User user = context.Users.Find(1);

            if (user != null && user.DatabaseInitialize)
            {
                return user.GameStarted;
            }
            
            try
            {
                await FranchiseGenerator.CreateDefaultFranchises(context);
                await PlayerGenerator.CreateDefaultPlayer(context);

                if (user == null)
                {
                    // First time they are opening the application or they wipe the DB.
                    User newUser = new User()
                    {
                        DatabaseInitialize = true,
                        GameStarted = false
                    };

                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    user = newUser;
                }
            }
            catch (Exception ex)
            {
                // TODO: Setup logger
                throw ex;
            }

            return user.GameStarted;
        }

        public static void CreateTables(MyLeagueFootballContext dbContext)
        {
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
        }
    }
}
