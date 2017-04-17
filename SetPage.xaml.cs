using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleLight.Resources;

namespace SimpleLight
{
    public partial class SetPage : PhoneApplicationPage
    {
        public SetPage()
        {
            InitializeComponent();
        }

        void AppBarInit()
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarBut_Save;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).Text = AppResources.AppBarBut_Cancel;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[2]).Text = AppResources.AppBarBut_Pin;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            check_set_start.IsChecked = AppData.Settings.start_on;
            check_set_lock.IsChecked = AppData.Settings.lock_on;
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void check_set_lock_Click(object sender, RoutedEventArgs e)
        {
            text_lock_afterrestart.Visibility = (AppData.Settings.lock_on != check_set_lock.IsChecked) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void AppBar_Save_Click(object sender, EventArgs e)
        {
            AppData.Settings.start_on = (bool)check_set_start.IsChecked;
            AppData.Settings.lock_on = (bool)check_set_lock.IsChecked;
            AppData.SetSave();
            NavigationService.GoBack();
        }

        private void AppBar_Cancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            FlipTileData TileData = new FlipTileData();
            TileData.BackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative);
            TileData.SmallBackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileSmall.png", UriKind.Relative);
            TileData.Title = "SimpleLight";
            TileData.BackTitle = "";
            TileData.BackBackgroundImage = null;
            TileData.WideBackBackgroundImage = null;
            TileData.WideBackgroundImage = null;
            ShellTile tiletopin = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("MainPage.xaml"));
            if (tiletopin == null)
            {
                ShellTile.Create(new Uri("/MainPage.xaml", UriKind.Relative), TileData, false);
            }
            else
            {
                MessageBox.Show(AppResources.Set_PinNo);
            }
        }
    }
}