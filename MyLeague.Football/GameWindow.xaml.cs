using MyLeague.Football.Core;
using MyLeague.Football.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            }
        }
    }
}
