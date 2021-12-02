using MyLeague.Football.Pages;
using System.Windows;

namespace MyLeague.Football
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ContentFrame.Navigate(new CreateLeaguePage());
        }
    }
}
