using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MyLeague.Football.Helpers;
using MyLeague.Football.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyLeague.Football
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public IEnumerable<NavigationViewItemBase> NavigationViewItems { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = "MyLeague Football";

            this.NavigationViewItems = this.GetInitialNavigationItems();
        }

        private void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(StartPage), this);
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                string tag = args.InvokedItemContainer.Tag.ToString();
                switch(tag)
                {
                    case Constants.NavigationTags.START:
                        if (ContentFrame.CurrentSourcePageType != typeof(StartPage))
                        {
                            ContentFrame.Navigate(typeof(StartPage), this);
                        }
                        break;

                    case Constants.NavigationTags.SETTINGS:
                        if (ContentFrame.CurrentSourcePageType != typeof(SettingsPage))
                        {
                            ContentFrame.Navigate(typeof(SettingsPage), this);
                        }
                        break;
                }
            }
        }

        private void HandleOnBack(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private List<NavigationViewItemBase> GetInitialNavigationItems()
        {
            return new List<NavigationViewItemBase>
            {
                new NavigationViewItem { Tag = Constants.NavigationTags.START, Content = "Create League", Icon = new SymbolIcon(Symbol.Home) }
            };
        }
    }
}
