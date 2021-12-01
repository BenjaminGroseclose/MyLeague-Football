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
