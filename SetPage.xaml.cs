using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleLight.Resources;
using System.ComponentModel;

namespace SimpleLight
{
    public partial class SetPage : PhoneApplicationPage
    {
        DataPage Data = new DataPage();

        public SetPage()
        {
            InitializeComponent();

            grid_root.DataContext = Data;
        }

        void AppBarInit()
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = AppResources.AppBarBut_Save;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).Text = AppResources.AppBarBut_Cancel;
            ((ApplicationBarIconButton)ApplicationBar.Buttons[2]).Text = AppResources.AppBarBut_Pin;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void AppBar_Save_Click(object sender, EventArgs e)
        {
            Settings.EnableOnStart = Data.Check_Start;
            Settings.LockDisable = Data.Check_Lock;
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

        class DataPage : INotifyPropertyChanged
        {
            bool Update = false;

            private bool _Check_Start;
            public bool Check_Start
            {
                get
                {
                    return _Check_Start;
                }
                set
                {
                    _Check_Start = value;
                    NotifyPropertyChanged("Check_Start");
                }
            }
            
            private bool _Check_Lock;
            public bool Check_Lock
            {
                get
                {
                    return _Check_Lock;
                }
                set
                {
                    _Check_Lock = value;
                    Vis_Restart = Update ? Visibility.Visible : Visibility.Collapsed;
                    NotifyPropertyChanged("Check_Lock");
                    NotifyPropertyChanged("Vis_Restart");
                }
            }
            
            public Visibility Vis_Restart { get; set; }

            public DataPage()
            {
                Check_Start = Settings.EnableOnStart;
                Check_Lock = Settings.LockDisable;
                Update = true;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}