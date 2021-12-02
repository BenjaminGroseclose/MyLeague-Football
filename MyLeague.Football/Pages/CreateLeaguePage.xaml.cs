using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyLeague.Football.Pages
{
    /// <summary>
    /// Interaction logic for CreateLeaguePage.xaml
    /// </summary>
    public partial class CreateLeaguePage : Page
    {
        public CreateLeaguePage()
        {
            InitializeComponent();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.FranchiseComboBox.Foreground = this.FranchiseComboBox.TryFindResource("MaterialDesignDarkForeground") as SolidColorBrush;
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.FranchiseComboBox.Foreground = this.FranchiseComboBox.TryFindResource("MaterialDesignLightForeground") as SolidColorBrush;
        }
    }
}
