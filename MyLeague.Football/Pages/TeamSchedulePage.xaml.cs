using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyLeague.Football.Pages
{
    /// <summary>
    /// Interaction logic for TeamSchedulePage.xaml
    /// </summary>
    public partial class TeamSchedulePage : Page
    {
        public TeamSchedulePage()
        {
            InitializeComponent();
        }

        private void FranchiseComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.FranchiseComboBox.Foreground = this.FranchiseComboBox.TryFindResource("MaterialDesignDarkForeground") as SolidColorBrush;
        }

        private void FranchiseComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.FranchiseComboBox.Foreground = this.FranchiseComboBox.TryFindResource("MaterialDesignLightForeground") as SolidColorBrush;
        }
    }
}
