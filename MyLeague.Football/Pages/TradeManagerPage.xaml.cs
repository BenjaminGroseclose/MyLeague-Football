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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyLeague.Football.Pages
{
    /// <summary>
    /// Interaction logic for TradeManagerPage.xaml
    /// </summary>
    public partial class TradeManagerPage : Page
    {
        public TradeManagerPage()
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

        private void ComputerPositionComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.ComputerPositionComboBox.Foreground = this.ComputerPositionComboBox.TryFindResource("MaterialDesignDarkForeground") as SolidColorBrush;
        }

        private void ComputerPositionComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.ComputerPositionComboBox.Foreground = this.ComputerPositionComboBox.TryFindResource("MaterialDesignLightForeground") as SolidColorBrush;
        }

        private void UserPositionComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.UserPositionComboBox.Foreground = this.UserPositionComboBox.TryFindResource("MaterialDesignDarkForeground") as SolidColorBrush;
        }

        private void UserPositionComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.UserPositionComboBox.Foreground = this.UserPositionComboBox.TryFindResource("MaterialDesignLightForeground") as SolidColorBrush;
        }
    }
}
