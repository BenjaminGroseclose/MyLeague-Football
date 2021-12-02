using MyLeague.Football.Core;
using MyLeague.Football.Pages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MyLeague.Football
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            Title = "MyLeague Football";
            this.ContentFrame.Navigate(new HomePage());
        }

        private void NavigationButton(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button == null)
            {
                throw new Exception("Unable to parse sender as button");
            }

            // TODO: Schedule Navigation should be a sub menu flow
            switch (button.Content)
            {
                case Constants.NavigationItems.HOME:
                    if (this.ContentFrame.Content.GetType() != typeof(HomePage))
                    {
                        this.ContentFrame.Navigate(new HomePage());
                    }
                    break;
                case Constants.NavigationItems.SCHEDULE:
                    if (this.ContentFrame.Content.GetType() != typeof(SchedulePage))
                    {
                        this.ContentFrame.Navigate(new SchedulePage());
                    }
                    break;
                case Constants.NavigationItems.TEAM_SCHEDULE:
                    if (this.ContentFrame.Content.GetType() != typeof(TeamSchedulePage))
                    {
                        this.ContentFrame.Navigate(new TeamSchedulePage());
                    }
                    break;
                case Constants.NavigationItems.ROSTER:
                    if (this.ContentFrame.Content.GetType() != typeof(RosterManagementPage))
                    {
                        this.ContentFrame.Navigate(new RosterManagementPage());
                    }
                    break;
            }
        }
    }
}
