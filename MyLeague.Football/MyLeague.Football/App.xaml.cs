using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using MyLeague.Football.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyLeague.Football
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window window;

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // Create DB
            string dbPath = await this.SetupDatabase();

            Services = this.ConfigureServices(dbPath);

            // Migrate DB
            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<MyLeagueFootballContext>();
                db.Database.Migrate();
            }

            window = new MainWindow();
            window.Activate();
        }

        private IServiceProvider ConfigureServices(string dbPath)
        {
            var services = new ServiceCollection();

            services.AddDbContext<MyLeagueFootballContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
            });

            return services.BuildServiceProvider();
        }

        private async Task<string> SetupDatabase()
        {
            var dbFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("MyLeague.db", CreationCollisionOption.OpenIfExists);
            return dbFile.Path;
        }
    }
}
