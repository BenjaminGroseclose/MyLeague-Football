using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyLeague.Football.Pages
{
    /// <summary>
    /// Interaction logic for RosterManagementPage.xaml
    /// </summary>
    public partial class RosterManagementPage : Page
    {
        public RosterManagementPage()
        {
            InitializeComponent();
        }

        private DataTemplate RosterCellTemplate;

        private void PositionComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.PositionComboBox.Foreground = this.PositionComboBox.TryFindResource("MaterialDesignDarkForeground") as SolidColorBrush;
        }

        private void PositionComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.PositionComboBox.Foreground = this.PositionComboBox.TryFindResource("MaterialDesignLightForeground") as SolidColorBrush;
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
