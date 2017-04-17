using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleLight.Resources;

namespace SimpleLight
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            AppBarInit();

            if (AppData.Settings.lock_on)
                PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;

            AppData.Light = new LightC(this);
        }

        void AppBarInit()
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarBut_Set;
        }

        private void but_flash_Click(object sender, RoutedEventArgs e)
        {
            AppData.Light.Change();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SetPage.xaml", UriKind.Relative));
        }
    }
}