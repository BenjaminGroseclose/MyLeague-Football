using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using MyLeague.Football.Data;
using MyLeague.Football.Data.API;
using MyLeague.Football.Data.Models;
using MyLeague.Football.Data.Repositories.Implementations;
using MyLeague.Football.Data.Repositories.Interfaces;
using MyLeague.Football.Services.Implementations;
using MyLeague.Football.Services.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyLeague.Football
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }

        public App()
        {
            this.ConfigureServices();

            bool leagueCreated;

            using (var scope = Ioc.Default.CreateScope())
            {
                MyLeagueFootballContext dbContext = scope.ServiceProvider.GetService<MyLeagueFootballContext>();

                League league = dbContext.Leagues.Find(1);

                leagueCreated = league != null;
            }

            if (leagueCreated)
            {
                StartupUri = new Uri("GameWindow.xaml", UriKind.Relative);
            }
            else
            {
                StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            }
            
        }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            //File.Create(path);

            services.AddDbContext<MyLeagueFootballContext>(options =>
            {
                options.UseSqlite($@"Data Source=MyLeague.db");
            });

            // Services
            services.AddTransient<ILeagueService, LeagueService>();

            // Repositories
            services.AddTransient<IFranchiseRepository, FranchiseRepository>();
            services.AddTransient<ILeagueRepository, LeagueRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();

            // APIs
            services.AddRefitClient<ISportsDataAPI>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.sportsdata.io/v3/nfl"));

            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }
    }
}
