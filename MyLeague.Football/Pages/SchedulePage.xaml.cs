using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyLeague.Football.Pages
{
    /// <summary>
    /// Interaction logic for SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();
        }

        private void WeekComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.WeekComboBox.Foreground = this.WeekComboBox.TryFindResource("MaterialDesignLightForeground") as SolidColorBrush;
        }

        private void WeekComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.WeekComboBox.Foreground = this.WeekComboBox.TryFindResource("MaterialDesignDarkForeground") as SolidColorBrush;
        }
    }
}
